using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyMinionPrefab;

    [SerializeField] private float spawnrate = 10f;

    [SerializeField] private Vector3 spawnLocationFromEnemy;

    private EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        InvokeRepeating("SpawnEnemyMinion", 0f, spawnrate);
    }

    private void SpawnEnemyMinion()
    {
        Vector3 enemySpawnLocation = transform.position + spawnLocationFromEnemy;
        GameObject enemyMinion = Instantiate(enemyMinionPrefab, enemySpawnLocation, Quaternion.identity);
        EnemyMovement minionMovement = enemyMinion.GetComponent<EnemyMovement>();
        minionMovement.SetTarget(enemyMovement.GetTarget());
    }
}
