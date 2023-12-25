using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkModeUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image imageToCopyColor;

    [SerializeField] private bool isStartRound;
    [SerializeField] private StartRound startRound;
    [SerializeField] private Color blue;
    private Color colorToHave;

    [SerializeField] private bool v2;

    private void Awake()
    {
        if (v2)
        {
            image = GetComponent<Image>();
            imageToCopyColor = GameObject.FindGameObjectWithTag("BackGround").GetComponent<Image>();
        }
    }

    private void LateUpdate()
    {
        if (isStartRound)
        {
            if (startRound.canStart)
            {
                colorToHave = blue;
            }
            else
            {
                colorToHave = imageToCopyColor.color;
                colorToHave.a = 0.5f;
            }
            image.color = Color.Lerp(image.color, colorToHave, 0.1f);
        }
        else
        {
            image.color = imageToCopyColor.color;
        }
    }
}
