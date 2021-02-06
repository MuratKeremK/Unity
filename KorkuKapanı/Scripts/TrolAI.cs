using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = System.Diagnostics.Debug;

public class TrolAI : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    GameObject playerObject;
    TrolSaglik trolSaglik;
    PlayerHealth playerHealth;
    Collider playerCollider;
    private GameObject trolBalyoz;
    Collider collision;
    int a = 1;
    int b = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindWithTag("Player");
        trolSaglik = GetComponent<TrolSaglik>();
        playerHealth =playerObject.GetComponent<PlayerHealth>();
        playerCollider = playerObject.GetComponent<CapsuleCollider>();

        trolBalyoz = GameObject.FindWithTag("TrolBalyoz");
        collision =trolBalyoz.GetComponent<CapsuleCollider>();
        

    }
    void Update()
    {
        AnimasyonKontrol();
    }

    /*private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Canın azaldı");
            playerHealth.DeductHealth(10);
        }
    }*/

    /*public void MakeAttack()
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          print("Canın azaldı");
          playerHealth.DeductHealth(10);
        }
    }
    */
    private void AnimasyonKontrol()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (40<distance&&distance<=70)
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", false);
            animator.SetBool("attack1", false);
            animator.SetBool("walk",true);
            agent.isStopped = false;
            transform.LookAt(playerObject.transform.position);
            agent.SetDestination(playerObject.transform.position);
        }
        
        else if(distance>70)
        {
            agent.isStopped = true;
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("attack1", false);
            animator.SetBool("idle",true);
        }
        
        if (distance>8 && distance<=40)
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", false);
            animator.SetBool("attack1", false);
            animator.SetBool("run", true);
            agent.isStopped = false;
            transform.LookAt(playerObject.transform.position);
            agent.SetDestination(playerObject.transform.position);
        }  
        else if (distance >= 3 && distance <= 8)
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", false);
            animator.SetBool("attack1", false);
            animator.SetBool("walk", true);
            agent.isStopped = false;
            transform.LookAt(playerObject.transform.position);
            agent.SetDestination(playerObject.transform.position);
        }
        if (1<=distance&&distance<3)
        {
           animator.SetBool("idle", false);
           animator.SetBool("walk", false);
           animator.SetBool("run", false);
            transform.LookAt(playerObject.transform.position);
            agent.SetDestination(playerObject.transform.position);
           agent.isStopped=true;
           animator.SetBool("attack1", true);
           
        }
        if (trolSaglik.GetHealth() <= 50)
        {
            if (b + 1 == 2)
            {
                animator.SetBool("idle", false);
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("attack1", false);
                agent.isStopped=true;
                animator.SetTrigger("takeDamage");
            }
            b++;
        }

        if (trolSaglik.GetHealth()<=0)
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("attack1", false);
            agent.isStopped = true;
            if (a+1==2)
            {
                animator.SetTrigger("death");
            }
            a++;
        }

       
        /*else if (distance<2)
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            agent.SetDestination(playerObject.transform.position);
            agent.isStopped = false;
            animator.SetBool("attack1", true);
        }*/
    }
  
}
