using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public bool onAttack;
    public int round;
    
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private TMPro.TMP_Text roundNSWE;
    [SerializeField] private Animator roundNSWEAnim;
    [SerializeField] private Transform borders;
    private Vector3 sizeToHave;
    [SerializeField] private EnemySpawner[] enemySpawner;

    [SerializeField] private Vector3[] sizes;

    [SerializeField] private GameObject UIPref;
    [SerializeField] private Transform UITransform;

    private void Start()
    {
        State.Set("Waiting");

        roundNSWE.text = "ROUND " + round.ToString() + ": About to start";
        roundNSWEAnim.SetTrigger("Change");


        onAttack = false;

        if (round >= 1 && round < 5) // [1, 5)
        {
            sizeToHave = sizes[0];
        }
        else if (round >= 5 && round < 10) // [5, 10)
        {
            sizeToHave = sizes[1];
        }
        else if (round >= 10 && round < 15) // [10, 15)
        {
            sizeToHave = sizes[2];
        }
        else if (round >= 15 && round < 30) // [15, 30)
        {
            sizeToHave = sizes[3];
        }
    }

    private void Update()
    {
        text.text = "-ROUND " + round.ToString() + "-";

        borders.localScale = Vector3.Lerp(borders.localScale, sizeToHave, 0.04f);

        if (onAttack && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            FinishRound();
        }
    }

    public void StartRound()
    {
        SaveManager.SaveS();
        State.Set("Attack");
        int j = Random.Range(0, 4);
        int i = Random.Range(0, 4);
        int a = Random.Range(0, 2);

        while (i == j)
        {
            i = Random.Range(0, 4);
        }

        bool both = a == 1;

        if (j == 0)
        {
            roundNSWE.text = "ROUND " + round.ToString() + ": North";
        }
        else if (j == 1)
        {
            roundNSWE.text = "ROUND " + round.ToString() + ": South";
        }
        else if (j == 2)
        {
            roundNSWE.text = "ROUND " + round.ToString() + ": East";
        }
        else if (j == 3)
        {
            roundNSWE.text = "ROUND " + round.ToString() + ": West";
        }

        if (both && round >= 10)
        {
            if (i == 0)
            {
                roundNSWE.text += " & North";
            }
            else if (i == 1)
            {
                roundNSWE.text += " & South";
            }
            else if (i == 2)
            {
                roundNSWE.text += " & East";
            }
            else if (i == 3)
            {
                roundNSWE.text += " & West";
            }
        }


        if (round >= 1 && round < 5) // [1, 5)
        {
            sizeToHave = sizes[0];
            
            enemySpawner[j].amountToSpawn = Random.Range(3, 5);
            enemySpawner[j].Spawn();

        }
        else if (round >= 5 && round < 10) // [5, 10)
        {
            sizeToHave = sizes[1];
            j += 1*4;
            enemySpawner[j].amountToSpawn = Random.Range(5, 6);
            enemySpawner[j].Spawn();
        }
        else if (round >= 10 && round < 15) // [10, 15)
        {
            sizeToHave = sizes[2];
            int amountToSpawn = Random.Range(10, 15);
            j += 2*4;
            i += 2*4;
            

            if (both)
            {
                enemySpawner[j].amountToSpawn = amountToSpawn / 2;
                enemySpawner[i].amountToSpawn = amountToSpawn / 2;
                enemySpawner[j].Spawn();
                enemySpawner[i].Spawn();
            }
            else
            {
                enemySpawner[j].amountToSpawn = amountToSpawn;
                enemySpawner[j].Spawn();
            }
        }
        else if (round >= 15 && round < 20) // [15, 20)
        {
            sizeToHave = sizes[3];
            int amountToSpawn = Random.Range(15, 21);
            j += 3 * 4;
            i += 3 * 4;


            if (both)
            {
                enemySpawner[j].amountToSpawn = amountToSpawn / 2;
                enemySpawner[i].amountToSpawn = amountToSpawn / 2;
                enemySpawner[j].Spawn();
                enemySpawner[i].Spawn();
            }
            else
            {
                enemySpawner[j].amountToSpawn = amountToSpawn;
                enemySpawner[j].Spawn();
            }
        }
        else if (round >= 20 && round < 30) // [15, 20)
        {
            sizeToHave = sizes[3];
            int amountToSpawn = Random.Range(21, round + 5);
            j += 4 * 4;
            i += 4 * 4;


            if (both)
            {
                enemySpawner[j].amountToSpawn = amountToSpawn / 2;
                enemySpawner[i].amountToSpawn = amountToSpawn / 2;
                enemySpawner[j].Spawn();
                enemySpawner[i].Spawn();
            }
            else
            {
                enemySpawner[j].amountToSpawn = amountToSpawn;
                enemySpawner[j].Spawn();
            }
        }

        roundNSWEAnim.SetTrigger("Change");

        onAttack = true;
    }

    private void FinishRound()
    {
        State.Set("Waiting");

        int add = Random.Range(round + 5, round + 11);

        GameObject uiPref = Instantiate(UIPref, UITransform);

        uiPref.GetComponentInChildren<TMPro.TMP_Text>().text = "+" + add.ToString();
        Coins.Add(add);
        Destroy(uiPref, 1f);


        roundNSWE.text = "ROUND " + round.ToString() + ": Finished!";
        roundNSWEAnim.SetTrigger("Change");

        round++;

        onAttack = false;

        SaveManager.SaveS();
        SaveManager.LoadS();
    }
}
