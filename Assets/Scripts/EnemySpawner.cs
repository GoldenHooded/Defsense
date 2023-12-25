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
            int random = Random.Range(0, enemies.Length - 1);
            GameObject inst = Instantiate(enemies[random], transform.position + Vector3.one * Random.Range(0.1f, 0.4f), enemies[random].transform.rotation);
            inst.transform.localScale = enemies[random].transform.localScale;
            inst.SetActive(true);
        }
    }
}
