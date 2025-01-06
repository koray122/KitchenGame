using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{

    public Transform prefab; // Mutfak nesnesinin prefab'ý
    public Sprite sprite; // Mutfak nesnesinin görsel temsili
    public string objectName; // Mutfak nesnesinin adý

}
