using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] private float timeBetwenAttacks;
    [SerializeField] private GameObject bulletPref;

    [SerializeField] private Enemy[] enemyArray;

    [SerializeField] private Structure structure;

    [SerializeField] private bool isOnCooldown = false;
    [SerializeField] private float coolDown;
    [SerializeField] private float cDRecoverSpeed;
    [SerializeField] private Image cDImg;

    [SerializeField] private Transform cannon;

    private bool doDrag;

    private void Awake()
    {
        Invoke("Attack", timeBetwenAttacks);
        Invoke("ResetCooldown", 0.01f);

        nuevaRotacion = Quaternion.identity;
    }

    private void ResetCooldown()
    {
        coolDown = 0;
    }

    private void Attack()
    {
        if (doDrag)
        {
            bool canReset = true;
            for (int i = 0; i < enemyArray.Length; i++)
            {
                if (enemyArray[i] != null)
                {
                    canReset = false; break;
                }
            }

            if (canReset)
            {
                Array.Resize(ref enemyArray, 0);
            }
            else //Hay enemigos
            {
                if (!isOnCooldown)
                {
                    Bullet bulletInst = Instantiate(bulletPref, transform.position, Quaternion.identity).GetComponent<Bullet>();
                    Enemy enemyToAdd = null;
                    for (int j = 0; j < enemyArray.Length; j++)
                    {
                        if (enemyArray[j] != null)
                        {
                            enemyToAdd = enemyArray[j]; break;
                        }
                    }

                    bulletInst.target = enemyToAdd; 
                }

                Invoke("Attack", timeBetwenAttacks);
            }
        }
    }

    private bool firstWithoutCooldown;
    Quaternion nuevaRotacion;

    private void Update()
    {
        transform.localPosition = Vector3.zero;

        doDrag = structure.doDrag;

        if (!doDrag)
        {
            coolDown = 1;
            firstWithoutCooldown = true;
        }

        for (int h = 0; h < enemyArray.Length; h++)
        {
            if (enemyArray[h] != null)
            {
                Debug.DrawLine(transform.position, transform.position + (enemyArray[h].transform.position - transform.position).normalized * 4.41f);
            }
        }

        if (coolDown > 0 && structure.doDrag)
        {
            coolDown -= Time.deltaTime * cDRecoverSpeed;
            isOnCooldown = true;
        } 
        else if (coolDown <= 0)
        {
            coolDown = 0;
            isOnCooldown = false;

            if (firstWithoutCooldown && !isOnCooldown)
            {
                CancelInvoke("Attack");
                Invoke("Attack", 0.01f);
                firstWithoutCooldown = false;
            }
        }

        cDImg.fillAmount = coolDown;

        for (int h = 0; h < enemyArray.Length; h++)
        {
            if (enemyArray[h] != null)
            {
                // Calcula la dirección desde este objeto hacia el objetivo
                Vector2 direccion = enemyArray[h].transform.position - cannon.position;

                // Normaliza la dirección para obtener un vector unitario
                direccion.Normalize();

                // Calcula el ángulo en radianes usando la función atan2 y conviértelo a grados
                float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

                angulo -= 90;

                Quaternion rotacionActual = cannon.rotation;

                // Calcula la rotación deseada
                Quaternion rotacionDeseada = Quaternion.Euler(new Vector3(0f, 0f, angulo));

                // Interpola suavemente entre la rotación actual y la deseada
                nuevaRotacion = Quaternion.Slerp(rotacionActual, rotacionDeseada, 5f * Time.deltaTime);

                // Aplica la nueva rotación al objeto
                
                break;
            }
        }

        cannon.rotation = nuevaRotacion;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            bool canReset = true;
            for (int i = 0; i < enemyArray.Length; i++)
            {
                if (enemyArray[i] != null)
                {
                    canReset = false; break;
                }
            }

            Enemy enemyToAdd = collision.gameObject.GetComponent<Enemy>();

            bool canAdd = true;

            for (int i = 0; i < enemyArray.Length; i++)
            {
                if (enemyArray[i] == enemyToAdd)
                {
                    canAdd = false; break;
                }
            }

            if (canAdd)
            {
                Array.Resize(ref enemyArray, enemyArray.Length + 1);
                enemyArray[enemyArray.Length - 1] = enemyToAdd;
            }

            if (canReset)
            {
                CancelInvoke("Attack");
                Invoke("Attack", 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            for (int i = 0; i < enemyArray.Length; i++)
            {
                if (enemyArray[i] == collision.GetComponent<Enemy>())
                {
                    enemyArray[i] = null;
                }
            }
        }
    }
}
