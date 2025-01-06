using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{

    // Mutfaðýn nesne takibini yapacak bir dönüþüm (transform) alýr.
    public Transform GetKitchenObjectFollowTransform();

    // Mutfaðýn nesnesini belirler ve onu bu nesneye yerleþtirir.
    public void SetKitchenObject(KitchenObject kitchenObject);

    // Bu mutfakta mevcut olan mutfak nesnesini getirir.
    public KitchenObject GetKitchenObject();

    // Bu mutfakta mevcut olan mutfak nesnesini temizler.
    public void ClearKitchenObject();

    // Bu mutfakta herhangi bir mutfak nesnesi olup olmadýðýný kontrol eder.
    public bool HasKitchenObject();

}
