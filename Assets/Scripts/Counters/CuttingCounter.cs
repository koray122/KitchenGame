using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{ // CuttingCounter s�n�f�, BaseCounter s�n�f�ndan t�retilir ve IHasProgress aray�z�n� uygular

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; // �lerleme de�i�ti�inde tetiklenen bir olay
    public event EventHandler OnCut; // Kesim i�lemi ger�ekle�ti�inde tetiklenen bir olay

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray; // Kesme tariflerini tutan dizi

    private int cuttingProgress; // Kesim i�leminin ilerleme durumu

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Burada herhangi bir Mutfak Nesnesi yok
            if (player.HasKitchenObject())
            {
                // Oyuncu bir �ey ta��yor
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Oyuncu kesilebilecek bir �ey ta��yor
                    player.GetKitchenObject().SetKitchenObjectParent(this); // Oyuncunun ta��d��� nesneyi bu tezgaha koy
                    cuttingProgress = 0; // Kesim ilerlemesini s�f�rla

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // �lerleme durumunu g�ncelle
                    });
                }
            }
            else
            {
                // Oyuncu bir �ey ta��m�yor
                // Buraya ba�ka i�lemler eklenebilir
            }
        }
        else
        {
            // Burada bir Mutfak Nesnesi var
            if (player.HasKitchenObject())
            {
                // Oyuncu bir �ey ta��yor
                // Buraya ba�ka i�lemler eklenebilir
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
                // Oyuncu bir �ey ta��m�yor
                GetKitchenObject().SetKitchenObjectParent(player); // Mevcut mutfak nesnesini oyuncuya ver
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // Burada bir Mutfak Nesnesi var ve kesilebilir
            cuttingProgress++; // Kesim ilerlemesini art�r

            OnCut?.Invoke(this, EventArgs.Empty); // Kesim olay�n� tetikle

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // �lerleme durumunu g�ncelle
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                // Kesim tamamland�
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf(); // Mevcut mutfak nesnesini yok et

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this); // Yeni kesilmi� mutfak nesnesini olu�tur
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null; // Kesim tarifinin olup olmad���n� kontrol et
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output; // Kesim tarifi varsa ��kt�s�n� d�nd�r
        }
        else
        {
            return null; // Kesim tarifi yoksa null d�nd�r
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO; // Giri� malzemesine uygun kesim tarifini d�nd�r
            }
        }
        return null; // Uygun kesim tarifi yoksa null d�nd�r
    }
}
