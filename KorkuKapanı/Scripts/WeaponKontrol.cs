using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKontrol : MonoBehaviour
{
    bool isStafe=false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("İS", isStafe);

        if (Input.GetKeyDown(KeyCode.F))
        {
            isStafe = !isStafe;
        }

        if (isStafe==true)
        {
            GetComponent<TpsKontrol>().hareketTipi = TpsKontrol.MovementType.Strafe;
        }
        if (isStafe == false)
        {
            GetComponent<TpsKontrol>().hareketTipi = TpsKontrol.MovementType.Directional;
        }
    }
}
