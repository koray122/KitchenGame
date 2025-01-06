using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{

    [SerializeField] private PlatesCounter platesCounter;  // PlatesCounter bileþenine referans
    [SerializeField] private Transform counterTopPoint;  // Tabaklarýn yerleþtirileceði nokta
    [SerializeField] private Transform plateVisualPrefab;  // Tabaklarýn görseli için prefab

    private List<GameObject> plateVisualGameObjectList;  // Tabaklarýn görsel nesnelerini tutacak liste

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();  // Listeyi baþlat
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;  // Tabak oluþturulma olayýný dinle
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;  // Tabak kaldýrýlma olayýný dinle
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        // Liste sonundaki tabak görselini al
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);  // Listeden kaldýr
        Destroy(plateGameObject);  // Görsel nesneyi yok et
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        // Tabak görselini oluþtur
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        // Tabaklarýn y ekseninde yerleþimini ayarla
        float plateOffsetY = .1f;  // Her tabak arasýnda 0.1 birim boþluk
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

        // Oluþturulan tabak görselini listeye ekle
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);


    }
}
