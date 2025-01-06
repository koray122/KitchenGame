using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BurningRecipeSO", fileName = "New BurningRecipe")]
public class BurningRecipeSO : ScriptableObject
{

    // Yanma i�lemi i�in kullan�lan malzemenin referans�
    public KitchenObjectSO input;

    // Yanma i�lemi sonucunda elde edilecek malzemenin referans�
    public KitchenObjectSO output;

    // Yanma i�lemi i�in gereken s�re (saniye cinsinden)
    public float burningTimerMax;

}
