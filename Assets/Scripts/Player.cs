using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    // Seçilen tezgah deðiþtiðinde tetiklenen olay
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter; // Seçilen tezgah
    }

    [SerializeField] private float moveSpeed = 7f; // Oyuncunun hareket hýzý
    [SerializeField] private GameInput gameInput; // Oyun giriþlerini yöneten sýnýf
    [SerializeField] private LayerMask countersLayerMask; // Tezgahlarý belirleyen katman maskesi
    [SerializeField] private Transform kitchenObjectHoldPoint; // Mutfak nesnelerinin tutulduðu nokta

    private bool isWalking; // Oyuncunun yürüyüp yürümadýðýný belirler
    private Vector3 lastInteractDir; // Son etkileþim yönü
    private BaseCounter selectedCounter; // Seçilen tezgah
    private KitchenObject kitchenObject; // Oyuncunun taþýdýðý mutfak nesnesi

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this; // Singleton pattern: Oyuncunun tek bir örneði olmasýný saðlar
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction; // Etkileþim olayýný dinler
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction; // Alternatif etkileþim olayýný dinler
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this); // Alternatif etkileþim gerçekleþirse
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this); // Etkileþim gerçekleþirse
        }
    }

    private void Update()
    {
        HandleMovement(); // Hareket iþlemlerini gerçekleþtir
        HandleInteractions(); // Etkileþim iþlemlerini gerçekleþtir
    }

    public bool IsWalking()
    {
        return isWalking; // Oyuncu yürüyorsa true döner
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // Hareket yönünü al

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // Hareket yönünü 3D vektör olarak oluþtur

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir; // Son etkileþim yönünü güncelle
        }

        float interactDistance = 2f; // Etkileþim mesafesi
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Tezgah varsa ve bu, seçili tezgahtan farklý ise
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter); // Yeni tezgahý seç
                }
            }
            else
            {
                SetSelectedCounter(null); // Tezgah bulunamadýysa seçimi temizle
            }
        }
        else
        {
            SetSelectedCounter(null); // Raycast ile tezgah bulunamadýysa seçimi temizle
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // Hareket yönünü al

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // Hareket yönünü 3D vektör olarak oluþtur

        float moveDistance = moveSpeed * Time.deltaTime; // Hareket mesafesini hesapla
        float playerRadius = .7f; // Oyuncu yarýçapý
        float playerHeight = 2f; // Oyuncu yüksekliði
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Eðer hareket edilemiyorsa sadece X ekseninde hareket etmeyi dene
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Yalnýzca X ekseninde hareket edilebiliyorsa, hareket yönünü güncelle
                moveDir = moveDirX;
            }
            else
            {
                // X ekseninde de hareket edilemiyorsa, yalnýzca Z ekseninde hareket etmeyi dene
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Yalnýzca Z ekseninde hareket edilebiliyorsa, hareket yönünü güncelle
                    moveDir = moveDirZ;
                }
                else
                {
                    // Hiçbir eksende hareket edilemiyorsa
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance; // Hareketi uygula
        }

        isWalking = moveDir != Vector3.zero; // Yürüyüp yürümadýðýný belirle

        float rotateSpeed = 10f; // Döndürme hýzý
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // Yönü döndür
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter; // Seçilen tezgahý güncelle

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter // Olayý tetikle
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint; // Mutfak nesnesinin tutma noktasýný döndür
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
        return kitchenObject != null; // Mutfak nesnesi olup olmadýðýný kontrol et
    }

}
