using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ates : MonoBehaviour
{
    #region tanımla
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed;
    [SerializeField] float coolDown = 0.1f;
    float lastFireTime = 0;
    public int defaultAmmo = 120;
    public int magSize = 30;
    public int currentAmmo;
    public int currentMagAmmo;
    #endregion

    void Start()
    {
        currentAmmo = defaultAmmo - magSize;
        currentMagAmmo = magSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (CanFire())
            {
                Saldırı(); 
            }
        }
    }
    private void Saldırı()
    {
     GameObject  myLazer= Instantiate(laser, transform.position, Quaternion.identity);
     Rigidbody fizik = myLazer.GetComponent<Rigidbody>();
     fizik.velocity = transform.TransformDirection(Vector3.forward * laserSpeed);
     currentMagAmmo -= 1;
     Debug.Log("Kalan Mermi" + currentMagAmmo);
    }
    private void Reload()
    {
        if (currentAmmo == 0 || currentMagAmmo == magSize)
        {
            return;
        }
        if (currentAmmo < magSize)
        {
            currentMagAmmo = currentMagAmmo + currentAmmo;
            currentAmmo = 0;
        }
        else
        {
            currentAmmo -= magSize - currentMagAmmo;
            currentMagAmmo = magSize;
        }
    }
    private bool CanFire()
    {
        if (currentMagAmmo > 0 && lastFireTime + coolDown < Time.time)
        {
            lastFireTime = Time.time + coolDown;
            return true;
        }
        return false;
    }
}
