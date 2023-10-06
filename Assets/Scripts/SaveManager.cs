using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Coins coinsScript;
    
    [Space(15)]
    [Header("Structures")]

    public GameObject castlePref;
    public GameObject towerPref;

    [SerializeField] private GameObject[] castles;
    [SerializeField] private GameObject[] towers;

    private void Awake()
    {
        Load();
        CameraDrag.CheckStructures();
    }

    #region OnApplication
    private void OnApplicationQuit()
    {
        Save();

        PlayerPrefs.Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        Save();

        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        Save();

        PlayerPrefs.Save();
    } 
    #endregion

    private void Save()
    {
        #region Structures
        castles = GameObject.FindGameObjectsWithTag("Castle");
        towers = GameObject.FindGameObjectsWithTag("Tower");

        PlayerPrefs.SetInt("castles.Length", castles.Length);
        PlayerPrefs.SetInt("towers.Length", towers.Length);

        for (int i = 0; i < castles.Length; i++)
        {
            PlayerPrefs.SetFloat("castles[" + i + "].transform.position.x", castles[i].transform.position.x);
            PlayerPrefs.SetFloat("castles[" + i + "].transform.position.y", castles[i].transform.position.y);
            PlayerPrefs.SetFloat("castles[" + i + "].transform.position.z", castles[i].transform.position.z);
        }
        for (int i = 0; i < towers.Length; i++)
        {
            PlayerPrefs.SetFloat("towers[" + i + "].transform.position.x", towers[i].transform.position.x);
            PlayerPrefs.SetFloat("towers[" + i + "].transform.position.y", towers[i].transform.position.y);
            PlayerPrefs.SetFloat("towers[" + i + "].transform.position.z", towers[i].transform.position.z);
        }
        #endregion

        PlayerPrefs.SetInt("Cash", Coins.Get());
        PlayerPrefs.SetInt("Health", Health.Get());
        PlayerPrefs.Save();
    }

    private void Load()
    {
        #region Structures
        //castles = GameObject.FindGameObjectsWithTag("Castle");
        towers = GameObject.FindGameObjectsWithTag("Tower");

        //int castlesLeft = PlayerPrefs.GetInt("castles.Length") - castles.Length;
        int towersLeft = PlayerPrefs.GetInt("towers.Length") - towers.Length;

        /*for (int i = 0; i < castlesLeft; i++)
        {
            Instantiate(castlePref);
        }*/
        for (int i = 0; i < towersLeft; i++)
        {
            Instantiate(towerPref);
        }

        //castles = GameObject.FindGameObjectsWithTag("Castle");
        towers = GameObject.FindGameObjectsWithTag("Tower");

        /*for (int i = 0; i < castles.Length; i++)
        {
            Vector3 pos = new Vector3
            (
                PlayerPrefs.GetFloat("castles[" + i + "].transform.position.x"),
                PlayerPrefs.GetFloat("castles[" + i + "].transform.position.y"),
                PlayerPrefs.GetFloat("castles[" + i + "].transform.position.z")
            );
            castles[i].transform.position = pos;
        }*/
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

        Coins.Set(PlayerPrefs.GetInt("Cash"));
        Health.Set(PlayerPrefs.GetInt("Health", 100));
        PlayerPrefs.Save();
    }
}
