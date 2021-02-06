using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerHiz : MonoBehaviour
{
    public float lazerSpeed;

  

    void Update()
    {
        //Vector3 position = this.transform.position;
        //position.x += lazerSpeed;
        //position.y += lazerSpeed;
        //position.z += lazerSpeed;
        //this.transform.position = position;


        Rigidbody fizik = GetComponent<Rigidbody>();
        fizik.AddForce(-transform.forward * lazerSpeed * Time.deltaTime);
    }
}
