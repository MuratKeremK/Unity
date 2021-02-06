using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public int GetHealthPlayer()
    {
        return currentHealth;
    }

    public void DeductHealth(int damageTrol)
    {
        currentHealth -= damageTrol;
        Debug.Log("Playerın CAnı azaldı" + currentHealth);
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        print("Öldün amk");
    }
}
