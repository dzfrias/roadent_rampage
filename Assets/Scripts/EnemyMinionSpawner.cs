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

    private List<GameObject> spawned;

    void Start()
    {
        StartCoroutine(Spawn());
        spawned = new List<GameObject>();
    }

    void OnValidate()
    {
        // Ensure y is equal to or greater than x
        spawnBetween = new Vector2(spawnBetween.x, Mathf.Max(spawnBetween.x, spawnBetween.y));
        distBetween = new Vector2(distBetween.x, Mathf.Max(distBetween.x, distBetween.y));
    }

    void Update()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            if (spawned[i] == null)
            {
                spawned.RemoveAt(i);
            }
        }
    }

    IEnumerator Spawn()
    {
        float timer = Random.Range(spawnBetween.x, spawnBetween.y);
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && spawned.Count < maxSpawn)
            {
                timer = Random.Range(spawnBetween.x, spawnBetween.y);
                GameObject minion = SpawnEnemyMinion();
                spawned.Add(minion);
            }
            yield return null;
        }
    }

    GameObject SpawnEnemyMinion()
    {
        Vector3 enemySpawn = (transform.position + (transform.forward * Random.Range(distBetween.x, distBetween.y))) + Vector3.right * Random.Range(-maxOffset, maxOffset);
        GameObject enemyMinion = Instantiate(enemyMinionPrefab, transform.position, Quaternion.identity);
        EnemyMovement minionMovement = enemyMinion.GetComponent<EnemyMovement>();
        minionMovement.SetTarget(transform);

        // Move to closest point in navmesh
        NavMeshHit hit;
        NavMesh.SamplePosition(enemySpawn, out hit, Mathf.Infinity, NavMesh.AllAreas);
        enemyMinion.GetComponent<NavMeshAgent>().Warp(hit.position);

        return enemyMinion;
    }
}
