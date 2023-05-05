using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyMinionPrefab;
    [SerializeField] private float maxOffset;
    [SerializeField] private Vector2 spawnBetween = new Vector2(10, 15);
    [SerializeField] private Vector2 distBetween = new Vector2(30, 50);
    [SerializeField] private int maxSpawn = 3;

    private int hasSpawned;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    void OnValidate()
    {
        // Ensure y is equal to or greater than x
        spawnBetween = new Vector2(spawnBetween.x, Mathf.Max(spawnBetween.x, spawnBetween.y));
        distBetween = new Vector2(distBetween.x, Mathf.Max(distBetween.x, distBetween.y));
    }

    IEnumerator Spawn()
    {
        float timer = Random.Range(spawnBetween.x, spawnBetween.y);
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && hasSpawned < maxSpawn)
            {
                timer = Random.Range(spawnBetween.x, spawnBetween.y);
                SpawnEnemyMinion();
                hasSpawned += 1;
            }
            yield return null;
        }
    }

    void SpawnEnemyMinion()
    {
        Vector3 enemySpawn = (transform.position + (transform.forward * Random.Range(distBetween.x, distBetween.y))) + Vector3.right * Random.Range(-maxOffset, maxOffset);
        GameObject enemyMinion = Instantiate(enemyMinionPrefab, transform.position, Quaternion.identity);
        EnemyMovement minionMovement = enemyMinion.GetComponent<EnemyMovement>();
        minionMovement.SetTarget(transform);

        // Move to closest point in navmesh
        NavMeshHit hit;
        NavMesh.SamplePosition(enemySpawn, out hit, Mathf.Infinity, NavMesh.AllAreas);
        enemyMinion.GetComponent<NavMeshAgent>().Warp(hit.position);
    }
}
