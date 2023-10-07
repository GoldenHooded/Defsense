using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Range(0, 100)] public int healthShown;
    private float health = 100;

    private float topStart;
    private float topShown;

    [SerializeField] float lerpSpeed;
    [SerializeField] private Animator anim;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        topStart = rectTransform.sizeDelta.y;
    }

    void UpdateHealthBar(float health)
    {
        // Ajusta la altura del RectTransform en función de la vida actual
        float newHeight = ((health - 100) / (float)100) * topStart;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);

        // Calcula el valor de top basándose en el valor inicial y la nueva altura
        float topValue = newHeight;
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -topValue);
    }

    void Update()
    {
        health = Mathf.Lerp(health, healthShown, lerpSpeed);

        UpdateHealthBar(health);

        if (healthShown <= 0)
        {
            anim.SetBool("Defeat", true);
        }
    }

    public static void Add(int coinsToAdd)
    {
        Health coinsScript = FindObjectOfType<Health>();

        coinsScript.healthShown += coinsToAdd;
    }

    public static void Set(int coinsToSet)
    {
        Health coinsScript = FindObjectOfType<Health>();

        coinsScript.healthShown = coinsToSet;
    }

    public static int Get()
    {
        Health coinsScript = FindObjectOfType<Health>();

        return coinsScript.healthShown;
    }
}