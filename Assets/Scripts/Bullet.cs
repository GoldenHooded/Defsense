using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public Enemy target;
    [SerializeField] private float velocidad = 5f; // Velocidad de movimiento del objeto
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool done = false;
    private bool done2 = false;
    private bool done3 = false;

    Vector3 direccion;
    private void Update()
    {
        if (target == null)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    if (Vector2.Distance((Vector2)enemies[i].transform.position, (Vector2)transform.position) < 0.75f)
                    {
                        target = enemies[i];
                        break;
                    }
                }
            }

            if (target == null)
            {
                // Mueve el objeto hacia el destino a la velocidad especificada
                transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

                if (!done3 )
                {
                    Destroy(target, 1f);
                    done3 = true;
                }
            }
        }
        else
        {
            done3 = false;

            if (!done)
            {
                direccion = target.transform.position - transform.position;

                // Normaliza la dirección para mantener una velocidad constante
                direccion.Normalize();

                // Mueve el objeto hacia el destino a la velocidad especificada
                transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

                if (Vector2.Distance(transform.position, target.transform.position) < 0.15f)
                {
                    done = true;
                }
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;

                if (!done2)
                {
                    target.health--;
                    target.White();
                    done2 = true;
                    Destroy(gameObject, 1f);
                }
            } 
        }
    }
}
