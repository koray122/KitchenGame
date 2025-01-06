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
        playerInputActions = new PlayerInputActions(); // PlayerInputActions sýnýfýndan bir nesne oluþturur
        playerInputActions.Player.Enable(); // Oyuncu kontrollerini etkinleþtirir

        playerInputActions.Player.Interact.performed += Interact_performed; // "Interact" eylemi gerçekleþtiðinde Interact_performed metodunu çaðýrýr
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed; // "InteractAlternate" eylemi gerçekleþtiðinde InteractAlternate_performed metodunu çaðýrýr
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty); // "InteractAlternate" eylemi gerçekleþtiðinde olay dinleyicilerine bilgi verir
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty); // "Interact" eylemi gerçekleþtiðinde olay dinleyicilerine bilgi verir
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // Oyuncunun hareket girdilerini alýr

        inputVector = inputVector.normalized; // Hareket vektörünü normalize eder, böylece yön bilgisi kaybolmaz ama büyüklük 1 olur

        return inputVector; // Normalized (standartlaþtýrýlmýþ) hareket vektörünü döner
    }

}
