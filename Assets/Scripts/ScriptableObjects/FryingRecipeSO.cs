using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{

    public KitchenObjectSO input; // Kızartılacak mutfak nesnesi
    public KitchenObjectSO output; // Kızartıldıktan sonra elde edilecek mutfak nesnesi
    public float fryingTimerMax; // Kızartma süresi (saniye cinsinden)

}
