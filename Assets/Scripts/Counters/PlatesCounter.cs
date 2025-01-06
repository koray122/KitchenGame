using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;  // Tabak oluþturulduðunda tetiklenecek olay
    public event EventHandler OnPlateRemoved;  // Tabak kaldýrýldýðýnda tetiklenecek olay

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;  // Tabak nesnesinin tanýmý

    private float spawnPlateTimer;  // Tabak oluþturma süresi
    private float spawnPlateTimerMax = 4f;  // Tabak oluþturma süresi sýnýrý
    private int platesSpawnedAmount;  // Oluþturulan tabak sayýsý
    private int platesSpawnedAmountMax = 4;  // Maksimum tabak sayýsý

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;  // Geçen süreyi güncelle

        if (spawnPlateTimer > spawnPlateTimerMax)
        {  // Süre sýnýrýna ulaþýldý mý?
            spawnPlateTimer = 0f;  // Sayacý sýfýrla

            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {  // Maksimum tabak sayýsýna ulaþýldý mý?
                platesSpawnedAmount++;  // Tabak sayýsýný artýr

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);  // Tabak oluþturulma olayýný tetikle
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {  // Oyuncu boþ ellerle mi?
            if (platesSpawnedAmount > 0)
            {  // Burada en az bir tabak var mý?
                platesSpawnedAmount--;  // Tabak sayýsýný azalt

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);  // Yeni tabak oluþtur ve oyuncuya ver

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);  // Tabak kaldýrýlma olayýný tetikle
            }
        }
    }

}
