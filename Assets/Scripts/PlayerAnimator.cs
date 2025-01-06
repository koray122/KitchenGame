using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking"; // Animator parametre ismi i�in bir sabit

    [SerializeField] private Player player; // Animat�r�n ba�l� oldu�u Player referans�

    private Animator animator; // Animator bile�eni

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator bile�enini al
    }

    private void Update()
    {
        // Player'�n y�r�y�p y�r�mad���n� kontrol et ve Animator'daki "IsWalking" parametresini ayarla
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

}
