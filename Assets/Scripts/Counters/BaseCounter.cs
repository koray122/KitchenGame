using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform counterTopPoint; // Mutfak nesnesinin konumland�r�laca�� nokta

    private KitchenObject kitchenObject; // Bu de�i�ken, bir mutfak nesnesini saklar

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();"); // Etkile�im oldu�unda hata mesaj� yazd�r�r (override edilmesi beklenir)
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();"); // Alternatif etkile�im i�in (override edilmesi beklenir)
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint; // Mutfak nesnesinin takip edece�i konumu d�ner
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject; // Bu metot, kitchenObject de�i�kenini verilen nesne ile ayarlar
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject; // Saklanan mutfak nesnesini d�ner
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null; // Saklanan mutfak nesnesini temizler, yani bo�alt�r
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null; // Mutfak nesnesinin olup olmad���n� kontrol eder ve sonucu d�ner
    }
}
