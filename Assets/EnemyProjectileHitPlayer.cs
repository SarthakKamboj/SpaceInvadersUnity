using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProjectileHitPlayer : MonoBehaviour
{
  	// Start is called before the first frame update
    void Start()
    {
			Destroy(gameObject, 4.0f);
    }

		void OnCollisionEnter2D(Collision2D col) {
			if (col.gameObject.tag == "Player") {
				// col.gameObject.GetComponent<EnemyDie>().Die();
				Debug.Log("hit player");
				// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				Destroy(gameObject);
			} else if (col.gameObject.tag != "enemy") {
				Destroy(gameObject);
			}
		}
}
