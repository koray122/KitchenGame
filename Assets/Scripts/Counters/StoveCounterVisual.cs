using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;  // Ýlgili StoveCounter referansý
    [SerializeField] private GameObject stoveOnGameObject;  // Ocak açýkken görünen nesne
    [SerializeField] private GameObject particlesGameObject;  // Partiküller efekti nesnesi

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;  // Durum deðiþtiðinde güncelleme yapýlacak
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;  // Görseli gösterme durumu
        stoveOnGameObject.SetActive(showVisual);  // Ocak açýk olduðunda veya kýzartma iþlemi sýrasýnda görünür
        particlesGameObject.SetActive(showVisual);  // Partiküller efekti de görünür
    }

}
