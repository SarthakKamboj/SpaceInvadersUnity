using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
		public EnemyInfo enemyInfo;
		EnemyManager enemyManager;

		void Start() {
			enemyManager = GameObject.Find("managers").GetComponent<EnemyManager>();
		}

		public void Die() {
			enemyManager.validEnemiesLevelsPerIdx[enemyInfo.enemyColIdx].Remove(enemyInfo.enemyRowIdx);
			enemyManager.numEnemiesInRow[enemyInfo.enemyRowIdx] -= 1;
			enemyManager.UpdateEnemyShooting();
			Destroy(gameObject);
		}
}
