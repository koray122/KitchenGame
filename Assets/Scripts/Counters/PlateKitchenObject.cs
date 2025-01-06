using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{


    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    // KitchenObjectSO türünde objeleri saklayan bir liste
    private List<KitchenObjectSO> kitchenObjectSOList;

    // Awake metodu, obje oluþturulduðunda çaðrýlýr
    private void Awake()
    {
        // Listeyi yeni bir KitchenObjectSO listesi olarak baþlatýr
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    // Bir KitchenObjectSO objesini listeye eklemeye çalýþýr
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {   if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        // Eðer liste bu objeyi zaten içeriyorsa
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Bu tür zaten var, eklemeyi baþarýsýz olarak iþaretle
            return false;
        }
        else
        {
            // Listeye yeni objeyi ekle
            kitchenObjectSOList.Add(kitchenObjectSO);
            // Ekleme iþlemi baþarýlý

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
