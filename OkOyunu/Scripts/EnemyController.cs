using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool onGround;
    private float widht;
    private Rigidbody2D myBody;
    [SerializeField] LayerMask engel;
    [SerializeField] float speed;
    private static int totalEnemyNumber=0;

    void Start()
    {
        totalEnemyNumber++;
        Debug.Log("Düşman ismi: " + gameObject.name + "oluştu" + "Oyundaki Toplam Düşman Sayısı = " + totalEnemyNumber);
        widht = GetComponent<SpriteRenderer>().bounds.extents.x;
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right*widht/2), Vector2.down, 2f,engel);
        if (hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (!onGround)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }

        myBody.velocity = new Vector2(transform.right.x * speed, 0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 playerRealPosition = transform.position + (transform.right * widht / 2);
        //Gizmos.DrawLine(playerRealPosition, playerRealPosition + new Vector3(0, -2f, 0));
    }
}
