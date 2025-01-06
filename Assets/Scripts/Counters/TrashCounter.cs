using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            // E�er oyuncunun �zerinde bir mutfak nesnesi varsa
            player.GetKitchenObject().DestroySelf();  // Bu nesneyi yok et
        }
    }

}
