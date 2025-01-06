using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    // Nesnenin kameraya nasýl bakacaðýný belirleyen enum (durum) tipi
    private enum Mode
    {
        LookAt,                // Kameraya bak
        LookAtInverted,        // Kameranýn tersine bak
        CameraForward,         // Kameranýn ileri yönüne bak
        CameraForwardInverted, // Kameranýn ileri yönünün tersine bak
    }

    [SerializeField] private Mode mode; // Ýnspektörde seçilebilecek bakýþ modu

    private void LateUpdate()
    {
        // Nesnenin bakýþ yönünü belirlemek için mod durumuna göre iþlemler yapýlýr
        switch (mode)
        {
            case Mode.LookAt:
                // Nesne, kameranýn bulunduðu noktayý hedef alýr
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                // Nesne, kameranýn bulunduðu noktayý tersinden hedef alýr
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                // Nesne, kameranýn ileri yönü ile ayný yönde olur
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                // Nesne, kameranýn ileri yönünün tersine bakar
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }

}
