using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;  // Tabak olu�turuldu�unda tetiklenecek olay
    public event EventHandler OnPlateRemoved;  // Tabak kald�r�ld���nda tetiklenecek olay

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;  // Tabak nesnesinin tan�m�

    private float spawnPlateTimer;  // Tabak olu�turma s�resi
    private float spawnPlateTimerMax = 4f;  // Tabak olu�turma s�resi s�n�r�
    private int platesSpawnedAmount;  // Olu�turulan tabak say�s�
    private int platesSpawnedAmountMax = 4;  // Maksimum tabak say�s�

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;  // Ge�en s�reyi g�ncelle

        if (spawnPlateTimer > spawnPlateTimerMax)
        {  // S�re s�n�r�na ula��ld� m�?
            spawnPlateTimer = 0f;  // Sayac� s�f�rla

            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {  // Maksimum tabak say�s�na ula��ld� m�?
                platesSpawnedAmount++;  // Tabak say�s�n� art�r

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);  // Tabak olu�turulma olay�n� tetikle
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {  // Oyuncu bo� ellerle mi?
            if (platesSpawnedAmount > 0)
            {  // Burada en az bir tabak var m�?
                platesSpawnedAmount--;  // Tabak say�s�n� azalt

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);  // Yeni tabak olu�tur ve oyuncuya ver

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);  // Tabak kald�r�lma olay�n� tetikle
            }
        }
    }

}
