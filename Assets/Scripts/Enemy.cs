using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public int value = 1;
    public int damage = 1;
    public int attackSpeed = 1;

    [SerializeField] GameObject coin;

    [SerializeField] private Transform[] pointsTransforms;
    private Vector3[] points;
    private int currentPointIndex = 0;

    [SerializeField] private float moveSpeed = 5f; // Velocidad constante de movimiento
    [SerializeField] private float raycastDistance = 0.1f; // Distancia del raycast para detectar obstáculos
    [SerializeField] private LayerMask obstacleLayer; // Capa de obstáculos

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        points = new Vector3[pointsTransforms.Length];
        for (int i = 0; i < pointsTransforms.Length; i++)
        {
            points[i] = pointsTransforms[i].position;
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Coins.Add(value);
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        if (currentPointIndex < points.Length)
        {
            Vector3 targetPosition = points[currentPointIndex];
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // Realiza un raycast hacia adelante para detectar obstáculos
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, raycastDistance, obstacleLayer);

            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Verifica si el objeto ha llegado al punto objetivo
            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                currentPointIndex++;
            }
        }
        else
        {
            Debug.Log("A");
            Vector3 targetPosition = new Vector3(0, 0, 0);
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // Realiza un raycast hacia adelante para detectar obstáculos
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, raycastDistance, obstacleLayer);

            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Verifica si el objeto ha llegado al punto objetivo
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPointIndex++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle"))
        {
            Invoke("Attack", 0.1f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Castle"))
        {
            CancelInvoke("Attack");
        }
    }

    private void Attack()
    {
        Health.Add(-damage);
        Invoke("Attack", attackSpeed);
    }
}
