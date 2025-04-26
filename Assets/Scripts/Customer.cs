using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customer : MonoBehaviour
{
    public Player player;
    public Menu menu;
    public Sprite[] villagers;
    public GameObject[] requestIcons;
    public TextMeshProUGUI quantityText;
    public Image villagerImage;
    int currentRequestSelection, currentRequestQuantity;
    public bool wheatOnly;

    private void Start()
    {
        NewRequest();
    }

    private void NewRequest()
    {
        villagerImage.sprite = villagers[Random.Range(0, villagers.Length)];
        currentRequestSelection = Random.Range(0, menu.canUnlock);
        if (wheatOnly) currentRequestSelection = 0;
        currentRequestQuantity = Random.Range(4, 12);
        currentRequestQuantity -= currentRequestSelection;
        requestIcons[currentRequestSelection].SetActive(true);
        quantityText.text = "x" + currentRequestQuantity;
    }

    public void CheckRequest()
    {
        if (player.plantAmts[currentRequestSelection] >= currentRequestQuantity)
        {
            player.plantAmts[currentRequestSelection] -= currentRequestQuantity;
            player.plantTexts[currentRequestSelection].text = "x" + player.plantAmts[currentRequestSelection];
            //if (player.plantAmts[currentRequestSelection] == 0) player.plantTexts[currentRequestSelection].color = Color.red;
            requestIcons[currentRequestSelection].SetActive(false);
            NewRequest();
        }
    }
}
