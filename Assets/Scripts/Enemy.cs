using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public int value = 1;

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

            /*if (hit.collider == null)
            {
                // No hay obstáculo, mueve el objeto
                
            }
            else
            {
                // Hay un obstáculo, detén el objeto
                rb.MovePosition(transform.position);
            }*/

            // Verifica si el objeto ha llegado al punto objetivo
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                currentPointIndex++;
            }
        }
    }
}
