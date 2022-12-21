using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
		public Transform target;

		public List<GameObject> enemyPrefabs;
		public int numEnemies = 10;
		public Transform leftBoundary, rightBoundary;
		public int numMovementStepsInOneDirection = 5;

		public float TimeBetweenMovements = 0.5f;
		float timeSinceLastMovement;
		float movementDistance;

		List<float> timesSinceLastMovement = new List<float>();
		List<int> dirs = new List<int>();
		List<int> stepsInCurDir = new List<int>();
		public List<int> numEnemiesInRow = new List<int>();
		public List<List<int>> validEnemiesLevelsPerIdx = new List<List<int>>();

		public float verticalSpaceBetweenEnemies = 0.5f;
		public float offsetTimeBetweenEnemies = 0.05f;

		List<GameObject> enemies = new List<GameObject>();

		// maybe change movement system to move based on constant percentage width of movement space
		// so num steps will change based on number of enemies in the row
    void Start()
    {
	 		float leftX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.1f, 0.0f, 0.0f)).x;
	 		float rightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.9f, 0.0f, 0.0f)).x;

			Vector3 leftPos = leftBoundary.position;
			leftPos.y = rightBoundary.position.y;
			leftPos.x = leftX;
			leftBoundary.position = leftPos;

			Vector3 rightPos = rightBoundary.position;
			rightPos.x = rightX;
			rightBoundary.position = rightPos;

			float topOfScreenWorldY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height,0)).y;
			float runningTopOffset = 0.0f;

			for (int j = 0; j < enemyPrefabs.Count; j++) {

				// Debug.Log("j: " + j);
				
				GameObject enemyPrefab = enemyPrefabs[j];
				// Debug.Log("before: " + timesSinceLastMovement.Count);
				timesSinceLastMovement.Add(TimeBetweenMovements - j * offsetTimeBetweenEnemies);
				// Debug.Log("before: " + timesSinceLastMovement.Count);
				dirs.Add(1);
				stepsInCurDir.Add(0);
				numEnemiesInRow.Add(numEnemies);

				GameObject go = Instantiate(enemyPrefab);
				Transform goTransform = go.transform;
				Renderer renderer = goTransform.Find("gfx").GetComponent<Renderer>();
				Vector3 enemyExtent = renderer.bounds.extents;
				float enemyWidth = enemyExtent.x * 2;
				runningTopOffset += verticalSpaceBetweenEnemies + (enemyExtent.y * 2);
				float totalMovementSpace = (rightBoundary.position.x - leftBoundary.position.x) - (numEnemies * enemyWidth);
				float spaceBetweenEnemies = totalMovementSpace / (numMovementStepsInOneDirection + numEnemies - 1);
				movementDistance = spaceBetweenEnemies;
				Destroy(go);

				// Debug.Log("enemyExtent: " + enemyExtent);
				// Debug.Log("renderer.bounds.extents.x * 2: " + renderer.bounds.extents.x * 2);
				// Debug.Log("goTransform.Find(\"gfx\").localScale" + goTransform.Find("gfx").localScale);
				// Debug.Log("goTransform.Find(\"gfx\").lossyScale" + goTransform.Find("gfx").lossyScale);
				// Debug.Log("goTransform.localScale: " + goTransform.localScale);
				// Debug.Log("totalMovementSpace: " + totalMovementSpace);
				// Debug.Log("spaceBetweenEnemies: " + spaceBetweenEnemies);
			 
				for (int i = 0; i < numEnemies; i++) {
					GameObject enemy = Instantiate(enemyPrefab);
					Transform enemyTransform = enemy.GetComponent<Transform>();
					Vector3 pos = enemyTransform.position;
					pos.y = topOfScreenWorldY - runningTopOffset;
					pos.x = leftPos.x + enemyExtent.x + ((spaceBetweenEnemies + enemyWidth) * i);
					enemyTransform.position = pos;
					enemies.Add(enemy);
					EnemyInfo enemyInfo = enemy.GetComponent<EnemyInfo>();
					enemyInfo.enemyRowIdx = j;
					enemyInfo.enemyColIdx = i;
				}
			}

			for (int i = 0; i < numEnemies; i++) {
				validEnemiesLevelsPerIdx.Add(new List<int>());
				for (int j = 0; j < enemyPrefabs.Count; j++) {
					validEnemiesLevelsPerIdx[i].Add(j);
				}
			}

			// Debug.Log("timesSinceLastMovement.Count: " + timesSinceLastMovement.Count);
			// Debug.Log("dirs.Count: " + dirs.Count);
			// Debug.Log("enemyPrefabs.Count: " + enemyPrefabs.Count);
			// Debug.Log("stepsInCurDir.Count: " + stepsInCurDir.Count);
			 
			UpdateEnemyShooting();
    }

    // Update is called once per frame
    void Update()
    {
			Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
      // Debug.Log("target is " + screenPos.y + " pixels from the top");
      // Debug.Log("Screen.height: " + Screen.height);

	 		// Debug.Log("Input.mousePosition: " + Input.mousePosition);
			target.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			for (int j = 0; j < enemyPrefabs.Count; j++) {
				timesSinceLastMovement[j] -= Time.deltaTime;

				if (timesSinceLastMovement[j] <= 0.0f) {
					timesSinceLastMovement[j] = TimeBetweenMovements;
					for (int i = 0; i < numEnemies; i++) {
						int idx = j * numEnemies + i;
						if (enemies[idx]) {
							enemies[idx].GetComponent<EnemyMove>().Move(dirs[j] * movementDistance);
						}
					}
					stepsInCurDir[j] += 1;

					if (stepsInCurDir[j] == numMovementStepsInOneDirection) {
						stepsInCurDir[j] = 0;
						dirs[j] *= -1;
					}

				}
			}

    }

		public void UpdateEnemyShooting() {
			for (int i = 0; i < enemies.Count; i++)	{
				if (enemies[i]) {
					enemies[i].GetComponent<EnemyShoot>().UpdateShootingEnable(this);
				}
			}
		}

		void CalculateNumSteps() {
			
		}
}
