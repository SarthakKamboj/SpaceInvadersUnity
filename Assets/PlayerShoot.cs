using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

		public GameObject playerProjectile;
		float yExtent;

    // Start is called before the first frame update
    void Start()
    {
			yExtent	= transform.Find("gfx").GetComponent<Renderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
			if (Input.GetKeyDown(KeyCode.Space)) {
				GameObject projectile = Instantiate(playerProjectile);
				projectile.GetComponent<Transform>().position = transform.position + new Vector3(0, yExtent + 0.01f, 0);
			}
    }
}
