using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Bu, ScriptableObject t�r�nde bir mutfak nesnesi

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Burada herhangi bir mutfak nesnesi yok
            if (player.HasKitchenObject())
            {
                // Oyuncu bir �ey ta��yor
                player.GetKitchenObject().SetKitchenObjectParent(this); // Oyuncunun ta��d��� nesneyi bu tezgaha koy
            }
            else
            {
                // Oyuncu bir �ey ta��m�yor
                // Buraya ekleyece�iniz ba�ka i�lemler olabilir
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
                // Oyuncu bir �ey ta��yor
                // Buraya ekleyece�iniz ba�ka i�lemler olabilir

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
                // Oyuncu bir �ey ta��m�yor
                GetKitchenObject().SetKitchenObjectParent(player); // Tezgahtaki nesneyi oyuncuya ver
            }
        }
    }

}
