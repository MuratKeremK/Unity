using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   
    #region tanımla
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    private float mySpeedx;
    private float mySpeedy;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround;
    private bool canDoubleJump;
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked;
    [SerializeField] float currentAttackedTimer;
    [SerializeField] float defaultAttackedTimer;
    private Animator myAnimator;
    [SerializeField] int arrowNumber;
    [SerializeField] Text arrowNumberText;
    [SerializeField] AudioClip dieMusic;
    [SerializeField] GameObject winPanel, losePanel;

    #endregion
    void Start()
    {
        myAnimator=gameObject.GetComponent<Animator>();
        attacked=false;
        myBody= GetComponent<Rigidbody2D>();
        defaultLocalScale=transform.localScale;
        arrowNumberText.text = arrowNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    #region sağa sola dönme
        mySpeedx=Input.GetAxis("Horizontal");
        myBody.velocity= new Vector2(mySpeedx*speed,myBody.velocity.y);
        if(mySpeedx<0){
                transform.localScale= new Vector3(-defaultLocalScale.x,defaultLocalScale.y,defaultLocalScale.z);
        }else if(mySpeedx>0){
               transform.localScale= new Vector3(defaultLocalScale.x,defaultLocalScale.y,defaultLocalScale.z);
        }
    #endregion

    #region anımatore bilgi gönderme
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedx));
    #endregion

    #region zıplama
        if(Input.GetKeyDown(KeyCode.Space)){
             if(onGround==true){
                 myBody.velocity=new Vector2(myBody.velocity.x,jumpPower);
                 canDoubleJump=true;
                 myAnimator.SetTrigger("Jump");
            }else{
             if(canDoubleJump==true){
                myBody.velocity=new Vector2(myBody.velocity.x,jumpPower); 
                canDoubleJump=false;
                myAnimator.SetTrigger("Jump");
              }
       
            }
        }
    #endregion

    #region ok atma
    if(Input.GetMouseButtonDown(0)&&arrowNumber>0){
        if(attacked==false){
            attacked=true;
            myAnimator.SetTrigger("Fire");
            Invoke("Attack",0.5f);
        }
            
    }

    if(attacked==true){
        currentAttackedTimer-=Time.deltaTime;
    }else{
        currentAttackedTimer=defaultAttackedTimer;
    }

    if(currentAttackedTimer<0){
        attacked=false;
    }
    #endregion
    }

    void Attack(){
        GameObject okumuz=Instantiate(arrow, transform.position, Quaternion.identity);
        okumuz.transform.parent = GameObject.Find("Arrows").transform;
      if(transform.localScale.x>0){
      okumuz.GetComponent<Rigidbody2D>().velocity=new Vector2(7f,0);
      }else{
          Vector3 okumuzScale= okumuz.transform.localScale;
          okumuz.transform.localScale= new Vector3(-okumuzScale.x,okumuzScale.y,okumuzScale.z);
          okumuz.GetComponent<Rigidbody2D>().velocity=new Vector2(-7f,0);
      }
        arrowNumber--;
        arrowNumberText.text = arrowNumber.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<TimeControl>().enabled = false;
            Die();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(Wait(true));
            Destroy(collision.gameObject);
        }
    }

    public void Die()
    {
        myAnimator.SetTrigger("Die");
        GameObject.Find("Sounds Controller").GetComponent<AudioSource>().clip = null;
        GameObject.Find("Sounds Controller").GetComponent<AudioSource>().PlayOneShot(dieMusic);
        myAnimator.SetFloat("Speed", 0f);
        //myBody.constraints = RigidbodyConstraints2D.FreezePosition;
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        StartCoroutine(Wait(false));



    }

    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0;
        if (win==true)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
        
    }
}
