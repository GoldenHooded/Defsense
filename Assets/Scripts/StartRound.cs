using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartRound : MonoBehaviour, IPointerDownHandler
{
    public bool canStart;
    private RoundManager roundManager;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        roundManager = FindObjectOfType<RoundManager>();
    }

    private void Update()
    {
        canStart = !roundManager.onAttack;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canStart) 
        { anim.SetTrigger("Click"); roundManager.StartRound(); }
    }
}
