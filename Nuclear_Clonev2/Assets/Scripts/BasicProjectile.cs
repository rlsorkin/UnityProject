using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            var enemyHealth = collision.gameObject.GetComponent<EnemyMove>();
            enemyHealth.takingDamage = true;
            enemyHealth.rb2d.AddForce(enemyHealth.knockBack * enemyHealth.forceMult, ForceMode2D.Impulse);
            enemyHealth.health -= 1;
        }
    }

}
