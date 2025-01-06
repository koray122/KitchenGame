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
    // KitchenObjectSO t�r�nde objeleri saklayan bir liste
    private List<KitchenObjectSO> kitchenObjectSOList;

    // Awake metodu, obje olu�turuldu�unda �a�r�l�r
    private void Awake()
    {
        // Listeyi yeni bir KitchenObjectSO listesi olarak ba�lat�r
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    // Bir KitchenObjectSO objesini listeye eklemeye �al���r
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {   if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        // E�er liste bu objeyi zaten i�eriyorsa
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Bu t�r zaten var, eklemeyi ba�ar�s�z olarak i�aretle
            return false;
        }
        else
        {
            // Listeye yeni objeyi ekle
            kitchenObjectSOList.Add(kitchenObjectSO);
            // Ekleme i�lemi ba�ar�l�

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
