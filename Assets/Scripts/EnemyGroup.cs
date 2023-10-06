using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        int random = Random.Range(1, 7);
        for (int i = 0; i < random; i++)
        {
            GameObject pref = Instantiate(prefab, transform);
            pref.transform.position = (Vector2)transform.position + new Vector2(Random.Range(-0.5f, 0.25f), Random.Range(-0.5f, 0.5f));
        }

        Invoke("Spawn", Random.Range(10f, 30f));
    }
}
