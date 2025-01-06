using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{

    public KitchenObjectSO input; // Kýzartýlacak mutfak nesnesi
    public KitchenObjectSO output; // Kýzartýldýktan sonra elde edilecek mutfak nesnesi
    public float fryingTimerMax; // Kýzartma süresi (saniye cinsinden)

}
