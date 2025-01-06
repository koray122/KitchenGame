using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{

    public KitchenObjectSO input; // Kesilecek mutfak nesnesinin referans�
    public KitchenObjectSO output; // Kesim sonucunda elde edilecek mutfak nesnesinin referans�
    public int cuttingProgressMax; // Kesim i�lemi i�in gereken darbe say�s�

}
