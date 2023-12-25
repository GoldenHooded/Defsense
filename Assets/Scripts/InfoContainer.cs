using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoContainer : MonoBehaviour
{
    [SerializeField] private GameObject container;

    public void Open()
    {
        container.SetActive(true);
    }

    public void Close()
    {
        container.SetActive(false);
    }
}
