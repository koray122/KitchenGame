using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            // Eðer oyuncunun üzerinde bir mutfak nesnesi varsa
            player.GetKitchenObject().DestroySelf();  // Bu nesneyi yok et
        }
    }

}
