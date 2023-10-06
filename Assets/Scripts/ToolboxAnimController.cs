using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolboxAnimController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Animator anim;

    public void OnPointerDown(PointerEventData eventData)
    {
        Change();
    }

    public void Change()
    {
        anim.SetTrigger("Change");
    }
}
