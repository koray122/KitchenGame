using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{

    public KitchenObjectSO input; // K�zart�lacak mutfak nesnesi
    public KitchenObjectSO output; // K�zart�ld�ktan sonra elde edilecek mutfak nesnesi
    public float fryingTimerMax; // K�zartma s�resi (saniye cinsinden)

}
