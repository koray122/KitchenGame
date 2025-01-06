using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;  // �lgili StoveCounter referans�
    [SerializeField] private GameObject stoveOnGameObject;  // Ocak a��kken g�r�nen nesne
    [SerializeField] private GameObject particlesGameObject;  // Partik�ller efekti nesnesi

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;  // Durum de�i�ti�inde g�ncelleme yap�lacak
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;  // G�rseli g�sterme durumu
        stoveOnGameObject.SetActive(showVisual);  // Ocak a��k oldu�unda veya k�zartma i�lemi s�ras�nda g�r�n�r
        particlesGameObject.SetActive(showVisual);  // Partik�ller efekti de g�r�n�r
    }

}
