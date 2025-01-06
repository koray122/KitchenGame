using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter; // Bu g�rselin ba�l� oldu�u BaseCounter
    [SerializeField] private GameObject[] visualGameObjectArray; // G�rsel elemanlar

    private void Start()
    {
        // Oyuncunun se�ili kar��l��� de�i�ti�inde �a�r�lacak metodu ayarla
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        // E�er se�ili kar��l�k baseCounter ile e�le�iyorsa g�rseli g�ster, aksi takdirde gizle
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
        // G�rsel elemanlar� aktif et
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        // G�rsel elemanlar� pasif yap
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

}
