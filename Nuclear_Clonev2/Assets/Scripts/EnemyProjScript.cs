using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            PlayerMove scr = coll.gameObject.GetComponent<PlayerMove>();
            scr.takingDamage = true;
            Destroy(gameObject);
        }

        if(coll.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}

