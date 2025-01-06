using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{

    [SerializeField] private PlatesCounter platesCounter;  // PlatesCounter bile�enine referans
    [SerializeField] private Transform counterTopPoint;  // Tabaklar�n yerle�tirilece�i nokta
    [SerializeField] private Transform plateVisualPrefab;  // Tabaklar�n g�rseli i�in prefab

    private List<GameObject> plateVisualGameObjectList;  // Tabaklar�n g�rsel nesnelerini tutacak liste

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();  // Listeyi ba�lat
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;  // Tabak olu�turulma olay�n� dinle
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;  // Tabak kald�r�lma olay�n� dinle
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        // Liste sonundaki tabak g�rselini al
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);  // Listeden kald�r
        Destroy(plateGameObject);  // G�rsel nesneyi yok et
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        // Tabak g�rselini olu�tur
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        // Tabaklar�n y ekseninde yerle�imini ayarla
        float plateOffsetY = .1f;  // Her tabak aras�nda 0.1 birim bo�luk
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

        // Olu�turulan tabak g�rselini listeye ekle
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);


    }
}
