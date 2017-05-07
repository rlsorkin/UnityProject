using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb2d;
    private Rigidbody2D projBody;
    public float movex;
    public float movey;
    Animator anim;
    public GameObject projectile;
    public float projectileSpeed = 10f;
    public bool ballGotten;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ballGotten = false;
    }

    private void Update()
    {
        if (ballGotten)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clicked");
                Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
                Vector2 direction = target - myPos;
                direction.Normalize();
                GameObject currProjectile = Instantiate(projectile, myPos, Quaternion.identity);

                projBody = currProjectile.GetComponent<Rigidbody2D>();
                projBody.velocity = direction * projectileSpeed;

            }
        }
    }

    void FixedUpdate () {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2(movex * speed, movey * speed);
        anim.SetFloat("speed", movex);
        anim.SetFloat("speedY", movey);

    
    }

}
