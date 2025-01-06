using System.Collections; // Koleksiyon sýnýflarýný kullanmak için gerekli
using System.Collections.Generic; // Genel koleksiyon sýnýflarýný kullanmak için gerekli
using UnityEngine; // Unity motoru ile ilgili sýnýflarý ve iþlevleri kullanmak için gerekli

public class ContainerCounterVisual : MonoBehaviour
{

    private const string OPEN_CLOSE = "OpenClose"; // Animatördeki "OpenClose" tetikleyicisi için sabit bir string

    [SerializeField] private ContainerCounter containerCounter; // Unity editöründe atanan ContainerCounter referansý

    private Animator animator; // Animator bileþeni için bir referans

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Bu GameObject üzerindeki Animator bileþenini al
    }

    private void Start()
    {
        // OnPlayerGrabbedObject olayýný dinlemeye baþla
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        // Olay tetiklendiðinde, animatörde "OpenClose" tetikleyicisini çalýþtýr
        animator.SetTrigger(OPEN_CLOSE);
    }

}
