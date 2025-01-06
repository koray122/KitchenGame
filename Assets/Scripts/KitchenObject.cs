using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Bu mutfak nesnesinin �zelliklerini tutan ScriptableObject.

    private IKitchenObjectParent kitchenObjectParent; // Bu mutfak nesnesinin ait oldu�u mutfak nesnesi ebeveyni.

    // Bu metod, mutfak nesnesinin �zelliklerini d�ner.
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    // Bu metod, mutfak nesnesinin ait oldu�u mutfak nesnesi ebeveynini ayarlar.
    // E�er �nceden bir ebeveyni varsa, onu temizler.
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

        // Nesneyi ebeveyninin d�n���m�ne yerle�tirir.
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    // Bu metod, mutfak nesnesinin ait oldu�u mutfak nesnesi ebeveynini d�ner.
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    // Bu metod, mutfak nesnesini yok eder ve ait oldu�u mutfak nesnesi ebeveynini temizler.
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

    // Bu statik metod, yeni bir mutfak nesnesi olu�turur ve ebeveynini ayarlar.
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); // Prefab'den yeni bir mutfak nesnesi olu�turur.

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>(); // Yeni mutfak nesnesinin `KitchenObject` bile�enini al�r.

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent); // Mutfak nesnesinin ebeveynini ayarlar.

        return kitchenObject;
    }

}
