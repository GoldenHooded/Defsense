using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int amountToSpawn;

    [SerializeField] private GameObject[] enemies;

    public void Spawn()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length - 1)], transform.position + Vector3.one * Random.Range(0.1f, 0.4f), Quaternion.identity);
        }
    }
}
