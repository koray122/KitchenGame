using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions(); // PlayerInputActions s�n�f�ndan bir nesne olu�turur
        playerInputActions.Player.Enable(); // Oyuncu kontrollerini etkinle�tirir

        playerInputActions.Player.Interact.performed += Interact_performed; // "Interact" eylemi ger�ekle�ti�inde Interact_performed metodunu �a��r�r
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed; // "InteractAlternate" eylemi ger�ekle�ti�inde InteractAlternate_performed metodunu �a��r�r
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty); // "InteractAlternate" eylemi ger�ekle�ti�inde olay dinleyicilerine bilgi verir
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty); // "Interact" eylemi ger�ekle�ti�inde olay dinleyicilerine bilgi verir
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // Oyuncunun hareket girdilerini al�r

        inputVector = inputVector.normalized; // Hareket vekt�r�n� normalize eder, b�ylece y�n bilgisi kaybolmaz ama b�y�kl�k 1 olur

        return inputVector; // Normalized (standartla�t�r�lm��) hareket vekt�r�n� d�ner
    }

}
