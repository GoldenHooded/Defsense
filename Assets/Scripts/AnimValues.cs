using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimValues : MonoBehaviour
{
    [SerializeField] private float alpha;

    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private Image[] images;

    [SerializeField] private bool enabledComp;
    [SerializeField] private MonoBehaviour[] components;

    void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, alpha);
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alpha);
        }

        for (int i = 0; i < components.Length; ++i)
        {
            components[i].enabled = enabledComp;
        }
    }
}
