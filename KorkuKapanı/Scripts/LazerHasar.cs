using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerHasar : MonoBehaviour
{
    public int hasar;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("çalıştır");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hasar verildi");
            //GameObject.Find("troll").GetComponent<TrolSaglik>().Hit(hasar);
            collision.GetComponent<TrolSaglik>().Hit(hasar);
        }
    }
}
