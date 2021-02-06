using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UzayliAI : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    GameObject playerObject;
    TrolSaglik trolSaglik;
    int a=1;

    Rigidbody rb;

    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed;
    GameObject yuvarlak;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindWithTag("Player");
        yuvarlak = GameObject.FindWithTag("UzayliLazerNokta");
        trolSaglik = GetComponent<TrolSaglik>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        AnimasyonKontrol();
    }
    public void Ates()
    {
        GameObject myLazer = Instantiate(laser, yuvarlak.transform.position, Quaternion.identity);
        Rigidbody fizik = myLazer.GetComponent<Rigidbody>();
        fizik.velocity = transform.TransformDirection(Vector3.forward * laserSpeed);
        Destroy(myLazer, 10);
    }
    private void AnimasyonKontrol()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (20 < distance && distance <= 40)
        {
            animator.SetBool("idle", false);
            animator.SetBool("flight", true);
            animator.SetBool("nisanAl", false);
            animator.SetBool("ates", false);
            agent.isStopped = false;
            transform.LookAt(playerObject.transform.position);
            agent.SetDestination(playerObject.transform.position);
        }
       else if(distance<40)
       {
            animator.SetBool("idle", true);
            animator.SetBool("flight", false);
            animator.SetBool("nisanAl", false);
            animator.SetBool("ates", false);
            agent.isStopped = false;
            
            agent.SetDestination(playerObject.transform.position);
       }
        if (distance<=20)
        {
            animator.SetBool("idle", false);
            animator.SetBool("flight", false);
            animator.SetBool("nisanAl", true);
            animator.SetBool("ates", true);
            agent.isStopped = true;
            transform.LookAt(playerObject.transform.position);
            agent.SetDestination(playerObject.transform.position);
        }
        if (trolSaglik.GetHealth() <= 0)
        {
            animator.SetBool("idle", false);
            animator.SetBool("flight", false);
            animator.SetBool("nisanAl", false);
            animator.SetBool("ates", false);
            agent.isStopped = true;
            if (a + 1 == 2)
            {
                animator.SetTrigger("death");
            }
            a++;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

    }
    
}
