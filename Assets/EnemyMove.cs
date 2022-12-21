using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
		public Transform enemyTransform;
		// public Rigidbody2D rb;

    public void Move(float offset)
    {
			Vector3 pos = enemyTransform.position;
			pos.x += offset;
			enemyTransform.position = pos;		
			// rb.MovePosition(pos);
		}

}
