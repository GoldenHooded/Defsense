using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject UIPref;
    [SerializeField] private Transform UITransform;
    [SerializeField] private int cost;
    [SerializeField] private Animator anim;
    [SerializeField] private TMPro.TMP_Text amount;
    [SerializeField] private int maxAmount = 5;

    [SerializeField] new private string tag;
    private int amountInt;

    private void Start()
    {
        amountInt = GameObject.FindGameObjectsWithTag(tag).Length;
        amount.text = amountInt.ToString() + "/" + maxAmount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.SetTrigger("Click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetTrigger("Down");
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetTrigger("Up");
    }

    public void ResetTriggers()
    {
        anim.ResetTrigger("Down");
        anim.ResetTrigger("Up");
        anim.ResetTrigger("Click");
    }

    private void Update()
    {
        amount.text = "" + amountInt.ToString() + "/" + maxAmount.ToString();
    }

    public void Buy()
    {
        amountInt = GameObject.FindGameObjectsWithTag(tag).Length;
        if (Coins.Get() >= cost && amountInt < maxAmount)
        {
            Coins.Add(-cost);

            Vector2 pos = new Vector2 (Camera.main.transform.position.x + Random.Range(-1.5f, 0), Camera.main.transform.position.y + Random.Range(-4, 4));
            Instantiate(spawnPrefab, pos, Quaternion.identity);

            GameObject uiPref = Instantiate(UIPref, UITransform);

            uiPref.GetComponentInChildren<TMP_Text>().text = "-" + cost.ToString();
            Destroy(uiPref, 1f);
            amountInt++;
        }
    }
}
