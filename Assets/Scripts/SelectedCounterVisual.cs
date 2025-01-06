using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter; // Bu görselin baðlý olduðu BaseCounter
    [SerializeField] private GameObject[] visualGameObjectArray; // Görsel elemanlar

    private void Start()
    {
        // Oyuncunun seçili karþýlýðý deðiþtiðinde çaðrýlacak metodu ayarla
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        // Eðer seçili karþýlýk baseCounter ile eþleþiyorsa görseli göster, aksi takdirde gizle
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        // Görsel elemanlarý aktif et
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        // Görsel elemanlarý pasif yap
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

}
