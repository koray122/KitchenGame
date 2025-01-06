using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    // Bir oyuncunun nesne ald��� zaman tetiklenen olay
    public event EventHandler OnPlayerGrabbedObject;

    // Scriptable Object t�r�nden bir de�i�ken
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Oyuncu ile etkile�im metodu, BaseCounter'dan override edilmi�
    public override void Interact(Player player)
    {
        // Oyuncu bir �ey ta��m�yorsa
        if (!player.HasKitchenObject())
        {
            // Mutfak nesnesini olu�tur ve oyuncuya ver
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            // Oyuncu nesne ald���nda OnPlayerGrabbedObject olay�n� tetikle
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
