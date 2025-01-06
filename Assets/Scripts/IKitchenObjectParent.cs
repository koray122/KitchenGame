using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{

    // Mutfa��n nesne takibini yapacak bir d�n���m (transform) al�r.
    public Transform GetKitchenObjectFollowTransform();

    // Mutfa��n nesnesini belirler ve onu bu nesneye yerle�tirir.
    public void SetKitchenObject(KitchenObject kitchenObject);

    // Bu mutfakta mevcut olan mutfak nesnesini getirir.
    public KitchenObject GetKitchenObject();

    // Bu mutfakta mevcut olan mutfak nesnesini temizler.
    public void ClearKitchenObject();

    // Bu mutfakta herhangi bir mutfak nesnesi olup olmad���n� kontrol eder.
    public bool HasKitchenObject();

}
