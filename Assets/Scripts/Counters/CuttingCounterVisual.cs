using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{

    private const string CUT = "Cut";  // Animasyonun adý "Cut" olarak tanýmlandý

    [SerializeField] private CuttingCounter cuttingCounter;  // Bu sýnýfa baðlý CuttingCounter referansý

    private Animator animator;  // Animasyon kontrolcüsü

    private void Awake()
    {
        animator = GetComponent<Animator>();  // Bu bileþen üzerindeki Animator bileþenini al
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;  // CuttingCounter'dan OnCut olayýný dinle
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);  // Kesim olayýný tetikleyince animasyonun "Cut" trigger'ýný çalýþtýr
    }
}
