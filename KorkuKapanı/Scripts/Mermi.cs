using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    #region tanımla
    public float coolDown = 0.1f;
    float lastFireTime = 0;
    public new Camera camera;
    public int range;
    [Header("Gun Damage On Hit")]
    public int damage;

    public GameObject bloodPrefab;
    public GameObject decal;
    public ParticleSystem muzzlePartical;

    public int defaultAmmo = 120;
    public int magSize = 30;
    public int currentAmmo;
    public int currentMagAmmo;

    Animator anim;
    GameObject playerObject;

    //int minAngle = -1;
    //int maxAngle = 1;
    #endregion
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        currentAmmo = defaultAmmo - magSize;
        currentMagAmmo = magSize;
        anim = playerObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (CanFire())
            {
                muzzlePartical.Emit(1);
                Fire();
                anim.SetTrigger("ates");
            }

        }
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
    private void Fire()
    {
        currentMagAmmo -= 1;
        Debug.Log("Kalan Mermi" + currentMagAmmo);
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 100))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<TrolSaglik>().Hit(damage);
                GenerateBloodEffect(hit);
            }
             else
             {
                 GenerateHitEffect(hit);
             }
        }
        //transform.localEulerAngles = new Vector3(Random.Range(minAngle, maxAngle), Random.Range(minAngle, maxAngle), Random.Range(minAngle, maxAngle));
    }
    
    private void GenerateHitEffect(RaycastHit hit)
    {
        GameObject hitObject = Instantiate(decal, hit.point, Quaternion.identity);
        hitObject.transform.rotation = Quaternion.FromToRotation(decal.transform.forward * -1, hit.normal);
    }
    private void GenerateBloodEffect(RaycastHit hit)
    {
        GameObject bloodObject = Instantiate(bloodPrefab, hit.point, hit.transform.rotation);
        // bloodPrefab.transform.position = hit.point;
        // bloodPrefab.SetActive(true);
    }
}
