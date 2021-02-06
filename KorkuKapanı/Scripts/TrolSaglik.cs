using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolSaglik : MonoBehaviour
{
    public int startHealth;
    private int currentHealth;
    
    void Start()
    {
        currentHealth = startHealth;
        Debug.Log("can :" + currentHealth);
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public void Hit(int damage)
    {
        currentHealth = currentHealth - damage;
        Debug.Log("can :" + currentHealth);
        if (currentHealth<=0)
        {
            Destroy(gameObject,10);
        }
    }
    
   
   
}
