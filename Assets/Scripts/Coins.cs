using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coins = 0;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public static void Add(int coinsToAdd)
    {
        Coins coinsScript = FindObjectOfType<Coins>();

        coinsScript.coins += coinsToAdd;
    }

    public static void Set(int coinsToSet)
    {
        Coins coinsScript = FindObjectOfType<Coins>();

        coinsScript.coins = coinsToSet;
    }

    public static int Get()
    {
        Coins coinsScript = FindObjectOfType<Coins>();

        return coinsScript.coins;
    }

    private void LateUpdate()
    {
        text.text = coins.ToString();
    }
}
