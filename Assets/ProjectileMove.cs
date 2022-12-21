using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
		private Transform projectileTransform;
		public float projectileSpeed = 10.0f;
		public Rigidbody2D rb;

    void Start()
    {
			projectileTransform = transform;	
			rb.AddForce(new Vector2(0, projectileSpeed), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
			// Vector3 pos = projectileTransform.position;
			// pos.y -= projectileSpeed * Time.fixedDeltaTime;
			// projectileTransform.position = pos;
			// rb.MovePosition(pos);
			// rb.AddForce(new Vector2(0, -projectileSpeed) - rb.velocity, ForceMode2D.Force);
    }
}
