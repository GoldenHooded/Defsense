using UnityEngine;
using UnityEngine.EventSystems;

public class DragControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool doDrag = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Cuando el dedo entra en la UI, deshabilita el arrastre
        doDrag = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Cuando el dedo sale de la UI, habilita el arrastre
        doDrag = true;
    }

    public bool CanDrag()
    {
        // Devuelve el estado actual de doDrag
        return Input.touchCount == 0 || doDrag;
    }
}
