using Unity.VisualScripting;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public bool doDrag;

    [SerializeField] private bool isLongPressed = false;
    [SerializeField] private bool isWall = false;
    [SerializeField] private bool isWall2 = false;
    private Wall wall;
    private float longPressDuration = 0.3f; // Duración del click prolongado en segundos
    private float pressTime = 0f;
    private bool touchStartedInsideObject = false; // Variable para rastrear si el toque comienza dentro del objeto
    private Vector3 offset; // Variable para almacenar la diferencia de posición entre el dedo y el centro del objeto

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource structureUpSound;
    [SerializeField] private AudioSource structureDownSound;

    [SerializeField] private CircleCollider2D collider2D_;

    [SerializeField] private bool hasRange;
    [SerializeField] private Animator rangeAnim;

    private DragControl dragControl;

    private void Awake()
    {
        CameraDrag.CheckStructures();
        dragControl = FindObjectOfType<DragControl>();

        wall = isWall ? GetComponentInParent<Wall>() : null;
    }

    void Update()
    {
        if (Input.touchCount > 0 && dragControl.doDrag)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Verifica si el toque comienza dentro del collider del objeto
                    if (collider2D_.OverlapPoint(touchPos))
                    {
                        pressTime = Time.time;
                        touchStartedInsideObject = true;
                        // Calcula la diferencia de posición entre el dedo y el centro del objeto
                        offset = transform.position - new Vector3(touchPos.x, touchPos.y, transform.position.z);
                    }
                    else
                    {
                        touchStartedInsideObject = false;
                    }
                    break;

                case TouchPhase.Moved:
                    // Si el toque se mueve fuera del objeto, cancela el clic prolongado
                    if (!collider2D_.OverlapPoint(touchPos))
                    {
                        touchStartedInsideObject = false;
                        isLongPressed = false;
                    }
                    else if (touchStartedInsideObject && Time.time - pressTime >= longPressDuration)
                    {
                        // Si el toque comienza dentro del objeto y se mantiene durante el tiempo suficiente, permite mover el objeto
                        Vector3 newPosition = new Vector3(touchPos.x, touchPos.y, transform.position.z) + offset;
                        transform.position = newPosition;
                    }
                    break;

                case TouchPhase.Stationary:
                    // Si el toque está en posición estacionaria, verifica si se ha mantenido pulsado el tiempo suficiente
                    if (touchStartedInsideObject && Time.time - pressTime >= longPressDuration)
                    {
                        isLongPressed = true;
                    }
                    break;

                case TouchPhase.Ended:
                    // Restablece los flags cuando se levanta el dedo
                    touchStartedInsideObject = false;
                    isLongPressed = false;
                    break;
            }
        }

        if (animator != null)
        {
            animator.SetBool("Selected", isLongPressed);
        }
        if (hasRange)
        {
            rangeAnim.SetBool("Active", isLongPressed);
        }
        collider2D_.radius = isLongPressed ? 3f : 1.725f; 
        doDrag = !isLongPressed;
        rb.bodyType = isLongPressed ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;

        if (isWall)
        {
            if (!isWall2)
            {
                wall.dragging = isLongPressed; 
            }
            else
            {
                wall.dragging2 = isLongPressed;
            }
        }
    }

    public void StructureUp()
    {
        structureUpSound.Play();
    }

    public void StructureDown()
    {
        structureDownSound.Play();
    }
}
