using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UzayliHasar : MonoBehaviour
{
    [Header("Gun Damage On Hit")]
    [SerializeField] int damage;

    PlayerHealth playerHealth;
    GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.DeductHealth(damage);
        }
    }
}
