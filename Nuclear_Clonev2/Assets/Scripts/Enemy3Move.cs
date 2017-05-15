using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Move : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb2d;
    public GameObject target;
    public GameObject batteryItem;
    public GameObject enemyProj;
    public Rigidbody2D enemyProjBody;
    public float projectileSpeed;

    public float cooldown;
    public float moveSpeed;
    public float health;
    public float aggroRange;
    public bool isDying;
    private float startTime;
    private bool canPerform;
    private Vector2 randomTarget;
    public bool takingDamage = false;

    Vector2 currentTargetPos;
    public Vector2 knockBack;
    public float forceMult = 1;
    public bool canShoot;

    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
        startTime = Time.time;
        randomTarget = transform.position;
        health = 5;
        canShoot = false;
        aggroRange = 15;
        cooldown = 2;

    }


    void FixedUpdate()
    {
        
        if (Time.time - startTime > 2)
        {
            randomTarget.x = Random.Range(-1, 2);
            randomTarget.y = Random.Range(-1, 2);
        }

        if(Time.time - startTime > 3)
        {
            startTime = Time.time;
            canShoot = true;
        }

        if (canShoot && inRange())
        {
            currentTargetPos = target.transform.position;
            Vector3 vectorToTarget = (Vector3)currentTargetPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = currentTargetPos - myPos;
            direction.Normalize();
            GameObject currProjectile = Instantiate(enemyProj, myPos, q);

            enemyProjBody = currProjectile.GetComponent<Rigidbody2D>();
            enemyProjBody.velocity = direction * projectileSpeed;
            canShoot = false;
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

        var xdif = target.transform.position.x + transform.position.x;
        var ydif = target.transform.position.y + transform.position.y;
        knockBack = new Vector2(xdif, ydif).normalized;
    }

    public bool inRange()
    {
        float trueDist = Vector2.Distance(transform.position, target.transform.position);
        

        if ((trueDist) < aggroRange)
        {
            Debug.Log(trueDist + " " + true);
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
        GameObject newBatt;
        var thisPos = transform.position;
        var thisRotate = transform.rotation;
        newBatt = Instantiate(batteryItem, thisPos, thisRotate);
        GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }

    private float GetNewSpeed(float health, float moveSpeed)
    {
        var intHealth = System.Convert.ToInt16(health);
        switch (intHealth)
        {
            case 3:
                return moveSpeed;

            case 2:
                return moveSpeed + .01f;

            case 1:
                return moveSpeed + .02f;

            default:
                return moveSpeed;
        }
    }
}
