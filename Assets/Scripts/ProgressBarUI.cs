using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{

    [SerializeField] private GameObject hasProgressGameObject; // �lerleme durumunu izleyecek nesne
    [SerializeField] private Image barImage; // UI �zerindeki ilerleme �ubu�u (bar)

    private IHasProgress hasProgress; // �lerleme durumunu sa�layacak bile�en

    private void Start()
    {
        // �lerleme durumu bile�enini al
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have a component that implements IHasProgress!");
        }

        // �lerleme de�i�ti�inde �a�r�lacak metodu ayarla
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        // �lerleme �ubu�unu ba�lang��ta s�f�r olarak ayarla
        barImage.fillAmount = 0f;

        // �lerleme �ubu�unu gizle
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        // �lerleme durumuna g�re �ubu�un doluluk oran�n� g�ncelle
        barImage.fillAmount = e.progressNormalized;

        // �lerleme tamamland���nda ya da s�f�rland���nda �ubu�u gizle, aksi halde g�ster
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true); // UI eleman�n� g�ster
    }

    private void Hide()
    {
        gameObject.SetActive(false); // UI eleman�n� gizle
    }

}
