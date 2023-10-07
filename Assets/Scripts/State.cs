using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class State : MonoBehaviour
{
    public string text;

    public static void Set(string textToSet)
    {
        FindObjectOfType<State>().text = textToSet;
        FindObjectOfType<State>().GetComponent<Animator>().SetTrigger("Change");
    }

    public void Refresh()
    {
        GetComponent<TMP_Text>().text = text;
    }
}
