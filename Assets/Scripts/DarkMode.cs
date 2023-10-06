using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DarkMode : MonoBehaviour
{
    private bool on;

    public Image image;
    [SerializeField] private bool isCamera;
    [SerializeField] private new Camera camera;

    [SerializeField] private bool isDarkMode;
    [SerializeField] private Color gridColor;
    [SerializeField] private Tilemap grid;
    [SerializeField] private SpriteRenderer[] actLikeGrid;

    [SerializeField] private bool isSprite;
    [SerializeField] private bool isSpriteLayout;
    [SerializeField] private Color layaoutColorMult = new Color(172, 214, 255, 255);
    [SerializeField] private DarkMode darkmode;
    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        if (isDarkMode)
        {
            on = PlayerPrefs.GetInt("on", 0) == 1;
            if (on) GetComponent<Animator>().SetTrigger("Pressed");
        }
        darkmode = GameObject.FindGameObjectWithTag("DarkMode").GetComponent<DarkMode>();
        image = GameObject.FindGameObjectWithTag("DarkMode").GetComponentInChildren<Image>();
    }
    private void LateUpdate()
    {
        if (isCamera)
        {
            camera.backgroundColor = image.color - gridColor;
        }

        if (isSprite)
        {
            sprite.color = new Color(image.color.r, image.color.g, image.color.b, sprite.color.a);
        }

        if (isSpriteLayout)
        {
            sprite.color = new Color(darkmode.gridColor.r, darkmode.gridColor.g, darkmode.gridColor.b, sprite.color.a) * layaoutColorMult + gridColor;
        }

        if (isDarkMode)
        {
            grid.color = gridColor;
            for (int i = 0; i < actLikeGrid.Length; i++)
            {
                actLikeGrid[i].color = gridColor;
            }
        }
    }

    public void On()
    {
        on = !on;
        PlayerPrefs.SetInt("on", on == true ? 1 : 0);
    }
}
