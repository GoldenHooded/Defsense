using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private bool reset;

    [Space(20)]
    [SerializeField] private Coins coinsScript;
    
    [Space(15)]
    [Header("Structures")]

    public GameObject castlePref;
    public GameObject towerPref;
    public GameObject towerLv2Pref;
    public GameObject wallPref;

    [Space(15)]
    [SerializeField] private GameObject[] castles;
    [SerializeField] private GameObject[] towers;
    [SerializeField] private GameObject[] towersLv2;
    [SerializeField] private Wall[] walls;
    private void Awake()
    {
        if (reset)
        {
            PlayerPrefs.DeleteAll();
        }
        Load();
        CameraDrag.CheckStructures();
    }

    #region OnApplication
    private void OnApplicationQuit()
    {
        if (!FindObjectOfType<RoundManager>().onAttack) Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!FindObjectOfType<RoundManager>().onAttack) Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!FindObjectOfType<RoundManager>().onAttack) Save();
    }
    #endregion

    public static void SaveS()
    {
        FindObjectOfType<SaveManager>().Save();
    }
    public static void LoadS()
    {
        FindObjectOfType<SaveManager>().Load();
    }

    public void Save()
    {
        #region Structures
        towers = GameObject.FindGameObjectsWithTag("Tower");

        PlayerPrefs.SetInt("towers.Length", towers.Length);
        for (int i = 0; i < towers.Length; i++)
        {
            PlayerPrefs.SetFloat("towers[" + i + "].transform.position.x", towers[i].transform.position.x);
            PlayerPrefs.SetFloat("towers[" + i + "].transform.position.y", towers[i].transform.position.y);
            PlayerPrefs.SetFloat("towers[" + i + "].transform.position.z", towers[i].transform.position.z);
        }

        towersLv2 = GameObject.FindGameObjectsWithTag("TowerLV2");

        PlayerPrefs.SetInt("towersLv2.Length", towersLv2.Length);
        for (int i = 0; i < towersLv2.Length; i++)
        {
            PlayerPrefs.SetFloat("towersLv2[" + i + "].transform.position.x", towersLv2[i].transform.position.x);
            PlayerPrefs.SetFloat("towersLv2[" + i + "].transform.position.y", towersLv2[i].transform.position.y);
            PlayerPrefs.SetFloat("towersLv2[" + i + "].transform.position.z", towersLv2[i].transform.position.z);
        }


        walls = FindObjectsOfType<Wall>();

        PlayerPrefs.SetInt("walls.Length", walls.Length);
        for (int i = 0; i < walls.Length; i++)
        {
            PlayerPrefs.SetFloat("walls[" + i + "].wall1.position.x", walls[i].wall1.position.x);
            PlayerPrefs.SetFloat("walls[" + i + "].wall1.position.y", walls[i].wall1.position.y);
            PlayerPrefs.SetFloat("walls[" + i + "].wall1.position.z", walls[i].wall1.position.z);

            PlayerPrefs.SetFloat("walls[" + i + "].wall2.position.x", walls[i].wall2.position.x);
            PlayerPrefs.SetFloat("walls[" + i + "].wall2.position.y", walls[i].wall2.position.y);
            PlayerPrefs.SetFloat("walls[" + i + "].wall2.position.z", walls[i].wall2.position.z);
        }

        #endregion

        PlayerPrefs.SetInt("Round", FindObjectOfType<RoundManager>().round);
        PlayerPrefs.SetInt("Cash", Coins.Get());
        PlayerPrefs.SetInt("Health", Health.Get());
        PlayerPrefs.Save();
    }

    private void Load()
    {
        #region Structures
        towers = GameObject.FindGameObjectsWithTag("Tower");

        int towersLeft = PlayerPrefs.GetInt("towers.Length") - towers.Length;

        for (int i = 0; i < towersLeft; i++)
        {
            Instantiate(towerPref);
        }

        towers = GameObject.FindGameObjectsWithTag("Tower");

        for (int i = 0; i < towers.Length; i++)
        {
            Vector3 pos = new Vector3
            (
                PlayerPrefs.GetFloat("towers[" + i + "].transform.position.x"),
                PlayerPrefs.GetFloat("towers[" + i + "].transform.position.y"),
                PlayerPrefs.GetFloat("towers[" + i + "].transform.position.z")
            );
            towers[i].transform.position = pos;
        }

        ////////////

        towersLv2 = GameObject.FindGameObjectsWithTag("TowerLV2");

        int towersLV2Left = PlayerPrefs.GetInt("towersLv2.Length") - towersLv2.Length;

        for (int i = 0; i < towersLV2Left; i++)
        {
            Instantiate(towerLv2Pref);
        }

        towersLv2 = GameObject.FindGameObjectsWithTag("TowerLV2");

        for (int i = 0; i < towersLv2.Length; i++)
        {
            Vector3 pos = new Vector3
            (
                PlayerPrefs.GetFloat("towersLv2[" + i + "].transform.position.x"),
                PlayerPrefs.GetFloat("towersLv2[" + i + "].transform.position.y"),
                PlayerPrefs.GetFloat("towersLv2[" + i + "].transform.position.z")
            );
            towersLv2[i].transform.position = pos;
        }

        ////////////

        walls = FindObjectsOfType<Wall>();

        int wallsLeft = PlayerPrefs.GetInt("walls.Length") - walls.Length;

        for (int i = 0; i < wallsLeft; i++)
        {
            Instantiate(wallPref);
        }

        walls = FindObjectsOfType<Wall>();

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].health = 25;

            Vector3 posWall1 = new Vector3
            (
                PlayerPrefs.GetFloat("walls[" + i + "].wall1.position.x"),
                PlayerPrefs.GetFloat("walls[" + i + "].wall1.position.y"),
                PlayerPrefs.GetFloat("walls[" + i + "].wall1.position.z")
            );
            Vector3 posWall2 = new Vector3
            (
                PlayerPrefs.GetFloat("walls[" + i + "].wall2.position.x"),
                PlayerPrefs.GetFloat("walls[" + i + "].wall2.position.y"),
                PlayerPrefs.GetFloat("walls[" + i + "].wall2.position.z")
            );

            walls[i].wall1.position = posWall1;
            walls[i].wall2.position = posWall2;
        }

        #endregion

        FindObjectOfType<RoundManager>().round = PlayerPrefs.GetInt("Round", 1);
        Coins.Set(PlayerPrefs.GetInt("Cash", 30));
        Health.Set(PlayerPrefs.GetInt("Health", 100));
        PlayerPrefs.Save();
    }
}
