using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject UIPref;
    [SerializeField] private Transform UITransform;
    [SerializeField] private int cost;
    [SerializeField] private Animator anim;

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetTrigger("Click");
    }

    public void Buy()
    {
        if (Coins.Get() >= cost)
        {
            Coins.Add(-cost);

            Vector2 pos = new Vector2 (Camera.main.transform.position.x + Random.Range(-1.5f, 0), Camera.main.transform.position.y + Random.Range(-4, 4));
            Instantiate(spawnPrefab, pos, Quaternion.identity);

            GameObject uiPref = Instantiate(UIPref, UITransform);

            uiPref.GetComponentInChildren<TMP_Text>().text = "-" + cost.ToString();
            Destroy(uiPref, 0.1f);
        }
    }
}
