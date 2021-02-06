using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerYokEtme : MonoBehaviour
{

    [SerializeField] GameObject effect;
    private void OnTriggerEnter(Collider collision)
    {
        if (!(collision.gameObject.CompareTag("Player")))
        {
            Destroy(gameObject);
            Instantiate(effect,transform.position, Quaternion.identity);
            //if (collision.gameObject.CompareTag("Enemy"))
            //{
            //  Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
            //GameObject.Find("Level Manager").GetComponent<LevelManager>().AddScore(100);
            //  Destroy(collision.gameObject);

            // }
        }

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
