using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TpsKontrol : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed;

    [Range(1, 20)]
    public float StrafeTurnSpeed;

    float normalFov;
    public float SprintFov;
    public float attackFov;

    float InputX;
    float InputY;
    float maxSpeed;

    public Transform Model;

    Animator anim;
    public float speed;
    Vector3 StickDirection;
    Camera mainCam;
    
    public KeyCode SprintButtton= KeyCode.LeftShift;
    public KeyCode WalkButton=KeyCode.C;

    public enum MovementType
    {
        Directional,
        Strafe
    };

    public MovementType hareketTipi;

    //bool isStrafeMoving;
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;
    }
    void LateUpdate()
    {
        Movement();
        InputMove();
        InputRotation();
    }
    void Movement()
    {
        if (hareketTipi==MovementType.Strafe)
        {
            InputX = Input.GetAxis("Horizontal");
            InputY = Input.GetAxis("Vertical");

            anim.SetFloat("IX", InputX, damp, Time.deltaTime * 10);
            anim.SetFloat("IY", InputY, damp, Time.deltaTime * 10);
            

            var hareketEdiyor = InputX != 0 || InputY != 0;

            if (hareketEdiyor)
            {
                float yanCamera = mainCam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yanCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);
                anim.SetBool("starfeMoving", true);
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, attackFov, Time.deltaTime * 2);
            }
            else
            {
                //Bu aralığı ben ekledim Hatalı olabilir
                float yanCamera = mainCam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yanCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);//
                anim.SetBool("starfeMoving", false);
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, attackFov, Time.deltaTime * 2);
            }

        }
        if (hareketTipi==MovementType.Directional)
        {
           // InputX =  Input.GetAxis("Horizontal");
            //InputY =  Input.GetAxis("Vertical");
            StickDirection = new Vector3(InputX, 0, InputY);
            if (Input.GetKey(SprintButtton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, SprintFov, Time.deltaTime * 2);
                maxSpeed = 2;
                InputX = 2 * Input.GetAxis("Horizontal");
                InputY = 2 * Input.GetAxis("Vertical");
            }
            else if (Input.GetKey(WalkButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
                maxSpeed = 0.2f;
                InputX = Input.GetAxis("Horizontal");
                InputY = Input.GetAxis("Vertical");
            }
            else
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
                maxSpeed = 1;
                InputX = Input.GetAxis("Horizontal");
                InputY = Input.GetAxis("Vertical");
            }
        }  
       
    }
    private void InputMove()
    {
        anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }
    private void InputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed);
    }
}
