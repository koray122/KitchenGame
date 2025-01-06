using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{

    public Transform prefab; // Mutfak nesnesinin prefab'�
    public Sprite sprite; // Mutfak nesnesinin g�rsel temsili
    public string objectName; // Mutfak nesnesinin ad�

}
