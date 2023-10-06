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

    private void Update()
    {
        if (!done && target != null)
        {
            Vector3 direccion = target.transform.position - transform.position;

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

            if (!done2 )
            {
                target.health--;
                done2 = true;
                Destroy(gameObject, 1f);
            }
        }

        if (target == null) Destroy(gameObject);
    }
}
