using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject pref;

    public void End()
    {
        Instantiate(pref, transform.position, Quaternion.identity);
        CameraDrag.CheckStructures();
        Handheld.Vibrate();
        Destroy(gameObject, 0.01f);
    }

    public void Sound()
    {
        GetComponent<AudioSource>().Play();
    }
}
