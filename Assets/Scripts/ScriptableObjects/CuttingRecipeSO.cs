using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{

    public KitchenObjectSO input; // Kesilecek mutfak nesnesinin referansý
    public KitchenObjectSO output; // Kesim sonucunda elde edilecek mutfak nesnesinin referansý
    public int cuttingProgressMax; // Kesim iþlemi için gereken darbe sayýsý

}
