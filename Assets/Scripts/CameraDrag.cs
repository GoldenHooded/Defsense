using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public bool doDrag;

    private Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    // Define los l�mites del �rea en la que la c�mara puede moverse usando los Transform de dos GameObjects
    public Transform cameraLimitTopLeft;
    public Transform cameraLimitBottomRight;

    public Structure[] structures;

    private DragControl dragControl;

    private void Awake()
    {
        structures = FindObjectsOfType<Structure>();
        dragControl = FindObjectOfType<DragControl>();
    }
    
    public static void CheckStructures()
    {
        CameraDrag camDrag = FindObjectOfType<CameraDrag>();

        camDrag.structures = FindObjectsOfType<Structure>();
    }

    void Update()
    {
        for (int i = 0; i < structures.Length; i++)
        {
            if (!structures[i].doDrag)
            {
                doDrag = false; break;
            }
            else
            {
                doDrag = true;
            }
        }

        if (!dragControl.CanDrag())
        {
            doDrag = false;
        }

        if (doDrag)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(touch.position) - Camera.main.ScreenToWorldPoint(touch.position + touch.deltaPosition);

                    // Calcula la nueva posici�n de la c�mara despu�s del movimiento
                    Vector3 newPosition = transform.position + new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0);

                    // Aseg�rate de que la nueva posici�n est� dentro de los l�mites
                    newPosition.x = Mathf.Clamp(newPosition.x, cameraLimitTopLeft.position.x, cameraLimitBottomRight.position.x);
                    newPosition.y = Mathf.Clamp(newPosition.y, cameraLimitBottomRight.position.y, cameraLimitTopLeft.position.y);

                    // Aplica la nueva posici�n a la c�mara
                    transform.position = newPosition;
                }
            }

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * 0.01f);
            } 
        }
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
