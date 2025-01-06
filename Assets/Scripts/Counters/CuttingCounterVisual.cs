using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{

    private const string CUT = "Cut";  // Animasyonun ad� "Cut" olarak tan�mland�

    [SerializeField] private CuttingCounter cuttingCounter;  // Bu s�n�fa ba�l� CuttingCounter referans�

    private Animator animator;  // Animasyon kontrolc�s�

    private void Awake()
    {
        animator = GetComponent<Animator>();  // Bu bile�en �zerindeki Animator bile�enini al
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;  // CuttingCounter'dan OnCut olay�n� dinle
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);  // Kesim olay�n� tetikleyince animasyonun "Cut" trigger'�n� �al��t�r
    }
}
