using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{

    // �lerleme durumu de�i�ti�inde tetiklenen olay
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    // Durum de�i�ti�inde tetiklenen olay
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    // Durum de�i�imi i�in veri s�n�f�
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;  // Yeni durum
    }

    // Frit�z�n olabilece�i durumlar
    public enum State
    {
        Idle,    // Bo�ta
        Frying,  // K�zart�l�yor
        Fried,   // K�zarm��
        Burned   // Yanm��
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray; // K�zartma tarifleri
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray; // Yanma tarifleri

    private State state;               // �u anki durum
    private float fryingTimer;        // K�zartma s�resi sayac�
    private FryingRecipeSO fryingRecipeSO;  // �u anki k�zartma tarifi
    private float burningTimer;       // Yanma s�resi sayac�
    private BurningRecipeSO burningRecipeSO;  // �u anki yanma tarifi

    private void Start()
    {
        state = State.Idle;  // Ba�lang��ta bo�ta
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    // Hi�bir i�lem yap�lmaz
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;  // K�zartma s�resini g�ncelle

                    // �lerleme durumunu g�ncelle
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        // K�zartma tamamland�
                        GetKitchenObject().DestroySelf();  // Mevcut mutfak nesnesini yok et

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);  // K�zarm�� nesneyi olu�tur

                        state = State.Fried;  // Durumu k�zarm�� olarak ayarla
                        burningTimer = 0f;  // Yanma s�recini s�f�rla
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        // Durum de�i�ikli�ini bildir
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;  // Yanma s�resini g�ncelle

                    // �lerleme durumunu g�ncelle
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        // Yanma tamamland�
                        GetKitchenObject().DestroySelf();  // Mevcut mutfak nesnesini yok et

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);  // Yanm�� nesneyi olu�tur

                        state = State.Burned;  // Durumu yanm�� olarak ayarla

                        // Durum de�i�ikli�ini bildir
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        // �lerleme durumunu s�f�rla
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    // Hi�bir i�lem yap�lmaz
                    break;
            }
        }
    }

    // Oyuncunun etkile�imde bulunmas� i�in ana fonksiyon
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Burada mutfak nesnesi yoksa
            if (player.HasKitchenObject())
            {
                // Oyuncu bir �ey ta��yorsa
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Oyuncunun ta��d��� �ey k�zart�labilir bir nesne ise
                    player.GetKitchenObject().SetKitchenObjectParent(this);  // Nesneyi bu tezgaha koy

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());  // Uygun k�zartma tarifini al

                    state = State.Frying;  // Durumu k�zartma olarak ayarla
                    fryingTimer = 0f;  // K�zartma zaman�n� s�f�rla

                    // Durum de�i�ikli�ini bildir
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    // �lerleme durumunu bildir
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
            }
            else
            {
                // Oyuncu bir �ey ta��m�yorsa
            }
        }
        else
        {
            // Burada bir mutfak nesnesi varsa
            if (player.HasKitchenObject())
            {
                // Oyuncu bir �ey ta��yorsa

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                   
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;  // Durumu bo�ta olarak ayarla

                        // Durum de�i�ikli�ini bildir
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        // �lerleme durumunu s�f�rla
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                }
            }
            else
            {
                // Oyuncu bir �ey ta��m�yorsa
                GetKitchenObject().SetKitchenObjectParent(player);  // Nesneyi oyuncuya ver

                state = State.Idle;  // Durumu bo�ta olarak ayarla

                // Durum de�i�ikli�ini bildir
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                // �lerleme durumunu s�f�rla
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    // Belirli bir nesne i�in uygun k�zartma tarifini d�nd�r�r
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    // Belirli bir nesne i�in ��kan nesneyi d�nd�r�r
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    // Belirli bir nesne i�in uygun k�zartma tarifini d�nd�r�r
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    // Belirli bir nesne i�in uygun yanma tarifini d�nd�r�r
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
