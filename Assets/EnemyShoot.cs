using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyShoot : MonoBehaviour
{
		const float TimeBetweenShotsMin = 1.0f;
		const float TimeBetweenShotsMax = 2.0f;
		[SerializeField] float timeSinceLastShot;
		public GameObject projectilePrefab;

		public Transform enemyTransform;
		public bool enabledShooting = false;

		public LayerMask enemyLayerMask;
		public EnemyInfo enemyInfo;

		float yExtent;

		void Start() {
			timeSinceLastShot = Random.Range(TimeBetweenShotsMin, TimeBetweenShotsMax);
			yExtent = enemyTransform.Find("gfx").GetComponent<Renderer>().bounds.extents.y;
			// UpdateShootingEnable();
		}

    // Update is called once per frame
    void Update()
    {
			if (!enabledShooting) {
				return;
			}
      timeSinceLastShot -= Time.deltaTime; 
			if (timeSinceLastShot <= 0.0f) {
			timeSinceLastShot = Random.Range(TimeBetweenShotsMin, TimeBetweenShotsMax);
				GameObject projectile = Instantiate(projectilePrefab);
				projectile.transform.position = enemyTransform.position;
			}
    }

		public void UpdateShootingEnable(EnemyManager enemyManager) {
			List<int> validRows = enemyManager.validEnemiesLevelsPerIdx[enemyInfo.enemyColIdx];
			if (validRows.Count > 0) {
				int max = validRows.Max();
				enabledShooting = max == enemyInfo.enemyRowIdx;
			}
			// RaycastHit2D hit = Physics2D.Raycast(new Vector2(enemyTransform.position.x, enemyTransform.position.y - yExtent - 0.02f), new Vector2(0, -1), enemyLayerMask);
			// enabledShooting = hit.collider == null;
			// if (enabledShooting) {
			// 	Debug.Log("enabledShooting for " + gameObject.name);
			// }
		}
}
