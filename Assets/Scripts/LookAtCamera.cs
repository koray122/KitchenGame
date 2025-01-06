using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    // Nesnenin kameraya nas�l bakaca��n� belirleyen enum (durum) tipi
    private enum Mode
    {
        LookAt,                // Kameraya bak
        LookAtInverted,        // Kameran�n tersine bak
        CameraForward,         // Kameran�n ileri y�n�ne bak
        CameraForwardInverted, // Kameran�n ileri y�n�n�n tersine bak
    }

    [SerializeField] private Mode mode; // �nspekt�rde se�ilebilecek bak�� modu

    private void LateUpdate()
    {
        // Nesnenin bak�� y�n�n� belirlemek i�in mod durumuna g�re i�lemler yap�l�r
        switch (mode)
        {
            case Mode.LookAt:
                // Nesne, kameran�n bulundu�u noktay� hedef al�r
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                // Nesne, kameran�n bulundu�u noktay� tersinden hedef al�r
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                // Nesne, kameran�n ileri y�n� ile ayn� y�nde olur
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                // Nesne, kameran�n ileri y�n�n�n tersine bakar
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }

}
