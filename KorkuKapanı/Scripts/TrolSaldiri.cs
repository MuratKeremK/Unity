using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolSaldiri : MonoBehaviour
{
    GameObject playerObject;
    PlayerHealth playerHealth;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Canın azaldı");
            playerHealth.DeductHealth(10);
        }
    }
}
