using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
		public Transform playerTransform;
		public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0.0f)
        {
					Vector3 pos = playerTransform.position;
					pos.x += horizontalInput * Time.deltaTime * speed;
					playerTransform.position = pos;
        }
    }
}
