using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public GameObject description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);
    }
}
