using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkModeUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image imageToCopyColor;

    private void LateUpdate()
    {
        image.color = imageToCopyColor.color;
    }
}
