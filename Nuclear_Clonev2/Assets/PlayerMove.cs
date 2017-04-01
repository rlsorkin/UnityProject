using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb2d;
    public float movex;
    public float movey;
    Animator anim;
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate () {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2(movex * speed, movey * speed);
    }
}
