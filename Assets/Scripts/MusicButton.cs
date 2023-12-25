using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool activeMusic = true;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("activeMusic", 1) == 0)
        {
            Change();
        }
    }

    public void Change()
    {
        anim.SetTrigger("Change");
        activeMusic = !activeMusic;
        PlayerPrefs.SetInt("activeMusic", activeMusic ? 1 : 0);
    }

    private void Update()
    {
        audioSource.enabled = activeMusic;
    }
}
