using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform counterTopPoint; // Mutfak nesnesinin konumlandýrýlacaðý nokta

    private KitchenObject kitchenObject; // Bu deðiþken, bir mutfak nesnesini saklar

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();"); // Etkileþim olduðunda hata mesajý yazdýrýr (override edilmesi beklenir)
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();"); // Alternatif etkileþim için (override edilmesi beklenir)
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint; // Mutfak nesnesinin takip edeceði konumu döner
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject; // Bu metot, kitchenObject deðiþkenini verilen nesne ile ayarlar
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject; // Saklanan mutfak nesnesini döner
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null; // Saklanan mutfak nesnesini temizler, yani boþaltýr
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null; // Mutfak nesnesinin olup olmadýðýný kontrol eder ve sonucu döner
    }
}
