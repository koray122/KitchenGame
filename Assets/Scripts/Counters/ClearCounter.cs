using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Bu, ScriptableObject türünde bir mutfak nesnesi

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Burada herhangi bir mutfak nesnesi yok
            if (player.HasKitchenObject())
            {
                // Oyuncu bir þey taþýyor
                player.GetKitchenObject().SetKitchenObjectParent(this); // Oyuncunun taþýdýðý nesneyi bu tezgaha koy
            }
            else
            {
                // Oyuncu bir þey taþýmýyor
                // Buraya ekleyeceðiniz baþka iþlemler olabilir
            }
        }
        else
        {
            // Burada bir mutfak nesnesi var
            if (player.HasKitchenObject())
            {

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                    
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    
                }
                // Oyuncu bir þey taþýyor
                // Buraya ekleyeceðiniz baþka iþlemler olabilir

                else
                {
                    if(GetKitchenObject().TryGetPlate(out  plateKitchenObject)){
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Oyuncu bir þey taþýmýyor
                GetKitchenObject().SetKitchenObjectParent(player); // Tezgahtaki nesneyi oyuncuya ver
            }
        }
    }

}
