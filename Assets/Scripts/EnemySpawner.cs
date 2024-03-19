using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    public GameObject enemyPrefab;
    public float spawnRate = 5.0f;

    private void Start()
    {
            InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }

    void SpawnEnemy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject(enemyPrefab.name, RandomSpawnPosition(), Quaternion.identity);
        }
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
    }
}


