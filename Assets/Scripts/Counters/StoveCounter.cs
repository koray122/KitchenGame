using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{

    // Ýlerleme durumu deðiþtiðinde tetiklenen olay
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    // Durum deðiþtiðinde tetiklenen olay
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    // Durum deðiþimi için veri sýnýfý
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;  // Yeni durum
    }

    // Fritözün olabileceði durumlar
    public enum State
    {
        Idle,    // Boþta
        Frying,  // Kýzartýlýyor
        Fried,   // Kýzarmýþ
        Burned   // Yanmýþ
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray; // Kýzartma tarifleri
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray; // Yanma tarifleri

    private State state;               // Þu anki durum
    private float fryingTimer;        // Kýzartma süresi sayacý
    private FryingRecipeSO fryingRecipeSO;  // Þu anki kýzartma tarifi
    private float burningTimer;       // Yanma süresi sayacý
    private BurningRecipeSO burningRecipeSO;  // Þu anki yanma tarifi

    private void Start()
    {
        state = State.Idle;  // Baþlangýçta boþta
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    // Hiçbir iþlem yapýlmaz
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;  // Kýzartma süresini güncelle

                    // Ýlerleme durumunu güncelle
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        // Kýzartma tamamlandý
                        GetKitchenObject().DestroySelf();  // Mevcut mutfak nesnesini yok et

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);  // Kýzarmýþ nesneyi oluþtur

                        state = State.Fried;  // Durumu kýzarmýþ olarak ayarla
                        burningTimer = 0f;  // Yanma sürecini sýfýrla
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        // Durum deðiþikliðini bildir
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;  // Yanma süresini güncelle

                    // Ýlerleme durumunu güncelle
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        // Yanma tamamlandý
                        GetKitchenObject().DestroySelf();  // Mevcut mutfak nesnesini yok et

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);  // Yanmýþ nesneyi oluþtur

                        state = State.Burned;  // Durumu yanmýþ olarak ayarla

                        // Durum deðiþikliðini bildir
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        // Ýlerleme durumunu sýfýrla
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    // Hiçbir iþlem yapýlmaz
                    break;
            }
        }
    }

    // Oyuncunun etkileþimde bulunmasý için ana fonksiyon
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Burada mutfak nesnesi yoksa
            if (player.HasKitchenObject())
            {
                // Oyuncu bir þey taþýyorsa
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Oyuncunun taþýdýðý þey kýzartýlabilir bir nesne ise
                    player.GetKitchenObject().SetKitchenObjectParent(this);  // Nesneyi bu tezgaha koy

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());  // Uygun kýzartma tarifini al

                    state = State.Frying;  // Durumu kýzartma olarak ayarla
                    fryingTimer = 0f;  // Kýzartma zamanýný sýfýrla

                    // Durum deðiþikliðini bildir
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    // Ýlerleme durumunu bildir
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
            }
            else
            {
                // Oyuncu bir þey taþýmýyorsa
            }
        }
        else
        {
            // Burada bir mutfak nesnesi varsa
            if (player.HasKitchenObject())
            {
                // Oyuncu bir þey taþýyorsa

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                   
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;  // Durumu boþta olarak ayarla

                        // Durum deðiþikliðini bildir
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        // Ýlerleme durumunu sýfýrla
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                }
            }
            else
            {
                // Oyuncu bir þey taþýmýyorsa
                GetKitchenObject().SetKitchenObjectParent(player);  // Nesneyi oyuncuya ver

                state = State.Idle;  // Durumu boþta olarak ayarla

                // Durum deðiþikliðini bildir
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                // Ýlerleme durumunu sýfýrla
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    // Belirli bir nesne için uygun kýzartma tarifini döndürür
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    // Belirli bir nesne için çýkan nesneyi döndürür
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

    // Belirli bir nesne için uygun kýzartma tarifini döndürür
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

    // Belirli bir nesne için uygun yanma tarifini döndürür
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
