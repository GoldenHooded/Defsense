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

    [Space(15)]
    [SerializeField] private GameObject[] castles;
    [SerializeField] private GameObject[] towers;
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
        #endregion

        FindObjectOfType<RoundManager>().round = PlayerPrefs.GetInt("Round", 1);
        Coins.Set(PlayerPrefs.GetInt("Cash", 30));
        Health.Set(PlayerPrefs.GetInt("Health", 100));
        PlayerPrefs.Save();
    }
}
