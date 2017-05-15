using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb2d;
    public GameObject target;
    public GameObject batteryItem;
    public float moveSpeed;
    public float health;
    public float aggroRange;
    public bool isDying;
    private float startTime;
    private bool canPerform;
    private Vector2 randomTarget;
    public bool takingDamage = false;

    public Vector2 knockBack;
    public float forceMult = 1;



    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startTime = Time.time;
        randomTarget = transform.position;
        health = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if(Time.time - startTime > 2)
        {
            randomTarget.x = Random.Range(-1, 2);
            randomTarget.y = Random.Range(-1, 2);
            startTime = Time.time;
        }

        anim.SetBool("takingDamage", takingDamage);

        if (takingDamage)
        {
            moveSpeed = GetNewSpeed(health, moveSpeed);
            //Debug.Log("Speed" + moveSpeed);
            Debug.Log("Health" + health);
            anim.SetBool("Moving", false);
            Invoke("setDamageAnimState", 1);
            takingDamage = false;
        }
        else
        {
            transform.Translate(randomTarget * moveSpeed);
            anim.SetBool("Moving", true);
        }
        
        if (health <= 0)
        {
           
            setDeath();
        }

        if (inRange())
        {
            //Debug.Log("In Range");
        }

        var xdif = target.transform.position.x + transform.position.x;
        var ydif = target.transform.position.y + transform.position.y;
        knockBack =  new Vector2(xdif, ydif).normalized;
    }

    public bool inRange()
    {
        float distanceX = rb2d.transform.position.x - target.transform.position.x;
        float distanceY = rb2d.transform.position.y - target.transform.position.y;
        float trueDist = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

        if (trueDist < aggroRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void patrol()
    {
        Vector2 initPos = transform.position;
        float rando = Random.Range(-1, 1);
        rb2d.transform.Translate(new Vector2((initPos.x * rando) * moveSpeed, (initPos.y * rando) * moveSpeed));
    }

    private void setDamageAnimState()
    {
        //rb2d.AddForce((-knockBack) * forceMult, ForceMode2D.Impulse);
        //Debug.Log(takingDamage);
        takingDamage = false;
    } 

    private void setDeath()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }

    private float GetNewSpeed(float health, float moveSpeed)
    {
        var intHealth = System.Convert.ToInt16(health);
        switch (intHealth)
        {
            case  3:
                return moveSpeed;
            
            case  2:
                return moveSpeed + .01f;
           
            case 1:
                return moveSpeed + .02f;
               
            default:
                return moveSpeed;
        }
    }
}
