using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
			Destroy(gameObject, 4.0f);
    }

		void OnCollisionEnter2D(Collision2D col) {
			if (col.gameObject.tag == "enemy") {
				col.gameObject.GetComponent<EnemyDie>().Die();
				Destroy(gameObject);
			}
		}

}
