using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    // Se�ilen tezgah de�i�ti�inde tetiklenen olay
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter; // Se�ilen tezgah
    }

    [SerializeField] private float moveSpeed = 7f; // Oyuncunun hareket h�z�
    [SerializeField] private GameInput gameInput; // Oyun giri�lerini y�neten s�n�f
    [SerializeField] private LayerMask countersLayerMask; // Tezgahlar� belirleyen katman maskesi
    [SerializeField] private Transform kitchenObjectHoldPoint; // Mutfak nesnelerinin tutuldu�u nokta

    private bool isWalking; // Oyuncunun y�r�y�p y�r�mad���n� belirler
    private Vector3 lastInteractDir; // Son etkile�im y�n�
    private BaseCounter selectedCounter; // Se�ilen tezgah
    private KitchenObject kitchenObject; // Oyuncunun ta��d��� mutfak nesnesi

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this; // Singleton pattern: Oyuncunun tek bir �rne�i olmas�n� sa�lar
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction; // Etkile�im olay�n� dinler
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction; // Alternatif etkile�im olay�n� dinler
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this); // Alternatif etkile�im ger�ekle�irse
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this); // Etkile�im ger�ekle�irse
        }
    }

    private void Update()
    {
        HandleMovement(); // Hareket i�lemlerini ger�ekle�tir
        HandleInteractions(); // Etkile�im i�lemlerini ger�ekle�tir
    }

    public bool IsWalking()
    {
        return isWalking; // Oyuncu y�r�yorsa true d�ner
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // Hareket y�n�n� al

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // Hareket y�n�n� 3D vekt�r olarak olu�tur

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir; // Son etkile�im y�n�n� g�ncelle
        }

        float interactDistance = 2f; // Etkile�im mesafesi
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Tezgah varsa ve bu, se�ili tezgahtan farkl� ise
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter); // Yeni tezgah� se�
                }
            }
            else
            {
                SetSelectedCounter(null); // Tezgah bulunamad�ysa se�imi temizle
            }
        }
        else
        {
            SetSelectedCounter(null); // Raycast ile tezgah bulunamad�ysa se�imi temizle
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // Hareket y�n�n� al

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // Hareket y�n�n� 3D vekt�r olarak olu�tur

        float moveDistance = moveSpeed * Time.deltaTime; // Hareket mesafesini hesapla
        float playerRadius = .7f; // Oyuncu yar��ap�
        float playerHeight = 2f; // Oyuncu y�ksekli�i
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // E�er hareket edilemiyorsa sadece X ekseninde hareket etmeyi dene
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Yaln�zca X ekseninde hareket edilebiliyorsa, hareket y�n�n� g�ncelle
                moveDir = moveDirX;
            }
            else
            {
                // X ekseninde de hareket edilemiyorsa, yaln�zca Z ekseninde hareket etmeyi dene
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Yaln�zca Z ekseninde hareket edilebiliyorsa, hareket y�n�n� g�ncelle
                    moveDir = moveDirZ;
                }
                else
                {
                    // Hi�bir eksende hareket edilemiyorsa
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance; // Hareketi uygula
        }

        isWalking = moveDir != Vector3.zero; // Y�r�y�p y�r�mad���n� belirle

        float rotateSpeed = 10f; // D�nd�rme h�z�
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // Y�n� d�nd�r
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter; // Se�ilen tezgah� g�ncelle

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter // Olay� tetikle
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint; // Mutfak nesnesinin tutma noktas�n� d�nd�r
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject; // Mutfak nesnesini ayarla
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject; // Mutfak nesnesini al
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null; // Mutfak nesnesini temizle
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null; // Mutfak nesnesi olup olmad���n� kontrol et
    }

}
