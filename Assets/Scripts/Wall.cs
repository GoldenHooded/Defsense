using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Transform wall1;
    public Transform wall2;
    [SerializeField] private Transform wallCollider;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject wall1Sub;
    [SerializeField] private GameObject wall2Sub;

    private LineRenderer lineRenderer;

    public int health = 25;

    public bool dragging;
    public bool dragging2;
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, wall1.transform.position);
        lineRenderer.SetPosition(1, wall2.transform.position);

        if (boxCollider)
        {
            boxCollider.size = new Vector2(Vector2.Distance(wall1.position, wall2.position) - 0.7f, 0.07f);

            wallCollider.transform.position = (wall1.transform.position + wall2.transform.position) / 2;

            Vector3 targetDirection = wall1.position - wallCollider.position;

            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            wallCollider.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (health < 0)
            {
                boxCollider.enabled = false;

                wall1Sub.SetActive(false);
                wall2Sub.SetActive(false);
            }
            else
            {
                boxCollider.enabled = true;

                float resultado = (float)health / 15f;
                lineRenderer.startColor = new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, resultado);
                lineRenderer.endColor = new Color(lineRenderer.endColor.r, lineRenderer.endColor.g, lineRenderer.endColor.b, resultado);
            }

            if (dragging)
            {
                boxCollider.enabled = false;

                lineRenderer.startColor = new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, 0.1f);
            }

            if (dragging2)
            {
                boxCollider.enabled = false;

                lineRenderer.endColor = new Color(lineRenderer.endColor.r, lineRenderer.endColor.g, lineRenderer.endColor.b, 0.1f);
            }
        }
    }
}
