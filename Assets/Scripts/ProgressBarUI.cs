using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{

    [SerializeField] private GameObject hasProgressGameObject; // Ýlerleme durumunu izleyecek nesne
    [SerializeField] private Image barImage; // UI üzerindeki ilerleme çubuðu (bar)

    private IHasProgress hasProgress; // Ýlerleme durumunu saðlayacak bileþen

    private void Start()
    {
        // Ýlerleme durumu bileþenini al
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have a component that implements IHasProgress!");
        }

        // Ýlerleme deðiþtiðinde çaðrýlacak metodu ayarla
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        // Ýlerleme çubuðunu baþlangýçta sýfýr olarak ayarla
        barImage.fillAmount = 0f;

        // Ýlerleme çubuðunu gizle
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        // Ýlerleme durumuna göre çubuðun doluluk oranýný güncelle
        barImage.fillAmount = e.progressNormalized;

        // Ýlerleme tamamlandýðýnda ya da sýfýrlandýðýnda çubuðu gizle, aksi halde göster
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
        gameObject.SetActive(true); // UI elemanýný göster
    }

    private void Hide()
    {
        gameObject.SetActive(false); // UI elemanýný gizle
    }

}
