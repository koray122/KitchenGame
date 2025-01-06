using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking"; // Animator parametre ismi için bir sabit

    [SerializeField] private Player player; // Animatörün baðlý olduðu Player referansý

    private Animator animator; // Animator bileþeni

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator bileþenini al
    }

    private void Update()
    {
        // Player'ýn yürüyüp yürümadýðýný kontrol et ve Animator'daki "IsWalking" parametresini ayarla
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

}
