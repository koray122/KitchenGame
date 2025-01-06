using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BurningRecipeSO", fileName = "New BurningRecipe")]
public class BurningRecipeSO : ScriptableObject
{

    // Yanma iþlemi için kullanýlan malzemenin referansý
    public KitchenObjectSO input;

    // Yanma iþlemi sonucunda elde edilecek malzemenin referansý
    public KitchenObjectSO output;

    // Yanma iþlemi için gereken süre (saniye cinsinden)
    public float burningTimerMax;

}
