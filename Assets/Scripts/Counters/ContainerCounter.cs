using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    // Bir oyuncunun nesne aldýðý zaman tetiklenen olay
    public event EventHandler OnPlayerGrabbedObject;

    // Scriptable Object türünden bir deðiþken
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Oyuncu ile etkileþim metodu, BaseCounter'dan override edilmiþ
    public override void Interact(Player player)
    {
        // Oyuncu bir þey taþýmýyorsa
        if (!player.HasKitchenObject())
        {
            // Mutfak nesnesini oluþtur ve oyuncuya ver
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            // Oyuncu nesne aldýðýnda OnPlayerGrabbedObject olayýný tetikle
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
