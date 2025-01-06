using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Bu mutfak nesnesinin özelliklerini tutan ScriptableObject.

    private IKitchenObjectParent kitchenObjectParent; // Bu mutfak nesnesinin ait olduðu mutfak nesnesi ebeveyni.

    // Bu metod, mutfak nesnesinin özelliklerini döner.
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    // Bu metod, mutfak nesnesinin ait olduðu mutfak nesnesi ebeveynini ayarlar.
    // Eðer önceden bir ebeveyni varsa, onu temizler.
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
        }

        kitchenObjectParent.SetKitchenObject(this);

        // Nesneyi ebeveyninin dönüþümüne yerleþtirir.
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    // Bu metod, mutfak nesnesinin ait olduðu mutfak nesnesi ebeveynini döner.
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    // Bu metod, mutfak nesnesini yok eder ve ait olduðu mutfak nesnesi ebeveynini temizler.
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if( this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {   
            plateKitchenObject=null;
            return false;
        }
    }

    // Bu statik metod, yeni bir mutfak nesnesi oluþturur ve ebeveynini ayarlar.
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); // Prefab'den yeni bir mutfak nesnesi oluþturur.

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>(); // Yeni mutfak nesnesinin `KitchenObject` bileþenini alýr.

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent); // Mutfak nesnesinin ebeveynini ayarlar.

        return kitchenObject;
    }

}
