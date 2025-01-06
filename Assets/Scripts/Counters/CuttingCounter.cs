using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{ // CuttingCounter sýnýfý, BaseCounter sýnýfýndan türetilir ve IHasProgress arayüzünü uygular

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; // Ýlerleme deðiþtiðinde tetiklenen bir olay
    public event EventHandler OnCut; // Kesim iþlemi gerçekleþtiðinde tetiklenen bir olay

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray; // Kesme tariflerini tutan dizi

    private int cuttingProgress; // Kesim iþleminin ilerleme durumu

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Burada herhangi bir Mutfak Nesnesi yok
            if (player.HasKitchenObject())
            {
                // Oyuncu bir þey taþýyor
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Oyuncu kesilebilecek bir þey taþýyor
                    player.GetKitchenObject().SetKitchenObjectParent(this); // Oyuncunun taþýdýðý nesneyi bu tezgaha koy
                    cuttingProgress = 0; // Kesim ilerlemesini sýfýrla

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // Ýlerleme durumunu güncelle
                    });
                }
            }
            else
            {
                // Oyuncu bir þey taþýmýyor
                // Buraya baþka iþlemler eklenebilir
            }
        }
        else
        {
            // Burada bir Mutfak Nesnesi var
            if (player.HasKitchenObject())
            {
                // Oyuncu bir þey taþýyor
                // Buraya baþka iþlemler eklenebilir
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                   
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
            }
            else
            {
                // Oyuncu bir þey taþýmýyor
                GetKitchenObject().SetKitchenObjectParent(player); // Mevcut mutfak nesnesini oyuncuya ver
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // Burada bir Mutfak Nesnesi var ve kesilebilir
            cuttingProgress++; // Kesim ilerlemesini artýr

            OnCut?.Invoke(this, EventArgs.Empty); // Kesim olayýný tetikle

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // Ýlerleme durumunu güncelle
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                // Kesim tamamlandý
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf(); // Mevcut mutfak nesnesini yok et

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this); // Yeni kesilmiþ mutfak nesnesini oluþtur
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null; // Kesim tarifinin olup olmadýðýný kontrol et
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output; // Kesim tarifi varsa çýktýsýný döndür
        }
        else
        {
            return null; // Kesim tarifi yoksa null döndür
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO; // Giriþ malzemesine uygun kesim tarifini döndür
            }
        }
        return null; // Uygun kesim tarifi yoksa null döndür
    }
}
