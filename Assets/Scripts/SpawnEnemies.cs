using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public float[] spawnPoints;
    public GameObject enemyPrefab;
    public float spawnRate = 2.0f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0.0f, spawnRate);
    }

    private void SpawnEnemy()
    {
        var randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        var randomPosition = new Vector3(spawnPoints[randomSpawnPoint], 0.3f, transform.position.z);
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
