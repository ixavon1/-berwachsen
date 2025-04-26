using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Player player;
    public GameObject menu;
    bool menuOpen, playerIn;
    public GameObject border;
    public int[] seedPrices;

    public int canUnlock;
    public int currentDifficulty;

    public void BuySeed(int id)
    {
        player.seedAmts[id]++;
        player.seedTexts[id].text = "x" + player.seedAmts[id];
        player.seedTexts[id].color = Color.white;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen && !Input.GetMouseButtonDown(0)) { menu.SetActive(false); menuOpen = false; }
            else if (!menuOpen && playerIn) {  menu.SetActive(true); menuOpen = true; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { playerIn = true; border.SetActive(true); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { playerIn = false; border.SetActive(false); }
    }
}
