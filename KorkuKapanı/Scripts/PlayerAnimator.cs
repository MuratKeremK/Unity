using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerHealth playerHealth;
    GameObject playerObject;
    int a = 1;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }


    void Update()
    {
        AnimasyonKontrol();
    }

    private void AnimasyonKontrol()
    {
        
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            animator.SetBool("tufekIdle", false);
            animator.SetInteger("tufek", 1);
        }
        else
        {
            animator.SetBool("tufekIdle", true);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)&&Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("tufekIdle", false);
            animator.SetInteger("tufek", 2);
        }
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("tufekIdle", false);
            animator.SetInteger("tufek", 3);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("tufekIdle", false);
            animator.SetTrigger("tufekJump");
        }
        if (playerHealth.GetHealthPlayer()<=0)
        {
            animator.SetBool("tufekIdle", false);
            animator.SetTrigger("tufekDeath");
        }
    }
}
