using System.Collections; // Koleksiyon s�n�flar�n� kullanmak i�in gerekli
using System.Collections.Generic; // Genel koleksiyon s�n�flar�n� kullanmak i�in gerekli
using UnityEngine; // Unity motoru ile ilgili s�n�flar� ve i�levleri kullanmak i�in gerekli

public class ContainerCounterVisual : MonoBehaviour
{

    private const string OPEN_CLOSE = "OpenClose"; // Animat�rdeki "OpenClose" tetikleyicisi i�in sabit bir string

    [SerializeField] private ContainerCounter containerCounter; // Unity edit�r�nde atanan ContainerCounter referans�

    private Animator animator; // Animator bile�eni i�in bir referans

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Bu GameObject �zerindeki Animator bile�enini al
    }

    private void Start()
    {
        // OnPlayerGrabbedObject olay�n� dinlemeye ba�la
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        // Olay tetiklendi�inde, animat�rde "OpenClose" tetikleyicisini �al��t�r
        animator.SetTrigger(OPEN_CLOSE);
    }

}
