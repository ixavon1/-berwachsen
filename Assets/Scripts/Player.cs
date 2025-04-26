using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public SpriteRenderer sr, shadowSr, reflectionSr;

    public float speed, accel;
    private float sprintMod;

    Vector2 input;

    public SpriteMask mask;

    public Health health;
    public Slider healthSlider;
    public Animator hitAnim;

    public float hitCooldown;

    bool inBed;
    public GameObject[] sprouts, seedPackets;
    public int[] seedAmts, plantAmts;
    private int currentSeed;
    public TextMeshProUGUI[] seedTexts, plantTexts;
    public GameObject plantText, selector;

    public Transform startPos;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        healthSlider.maxValue = health.maxHealth;
        healthSlider.value = health.currentHealth;

        for (int i = 0; i < seedTexts.Length; i++) { seedTexts[i].text = "x" + seedAmts[i]; if (seedAmts[i] == 0) { seedTexts[i].color = Color.red; } }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { input.x = -1; }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { input.x = 1; }
        else { input.x = 0; }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { input.y = 1; }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { input.y = -1; }
        else { input.y = 0; }

        mask.sprite = sr.sprite;
        shadowSr.sprite = sr.sprite;
        shadowSr.flipX = sr.flipX;
        reflectionSr.sprite = sr.sprite;
        reflectionSr.flipX = sr.flipX;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) sprintMod = 1.4f;
        else sprintMod = 1;

        if (inBed && Input.GetKeyDown(KeyCode.E) && seedAmts[currentSeed] > 0)
        {
            seedAmts[currentSeed]--;
            if (seedAmts[currentSeed] == 0) { seedTexts[currentSeed].color = Color.red; }
            seedTexts[currentSeed].text = "x" + seedAmts[currentSeed];
            GameObject sprout = Instantiate(sprouts[currentSeed], transform.position, Quaternion.identity);
            sprout.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentSeed++;
            currentSeed %= seedPackets.Length;
            selector.transform.position = seedPackets[currentSeed].transform.position;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentSeed--;
            if (currentSeed < 0) currentSeed = seedPackets.Length-1;
            currentSeed %= seedPackets.Length;
            selector.transform.position = seedPackets[currentSeed].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSeed = 0;
            selector.transform.position = seedPackets[currentSeed].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSeed = 1;
            selector.transform.position = seedPackets[currentSeed].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSeed = 2;
            selector.transform.position = seedPackets[currentSeed].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentSeed = 3;
            selector.transform.position = seedPackets[currentSeed].transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (hitCooldown > 0) hitCooldown -= Time.fixedDeltaTime;

        rb.velocity = Vector2.MoveTowards(rb.velocity, input.normalized * speed * sprintMod, accel * Time.fixedDeltaTime * sprintMod);

        anim.SetBool("walking", input != Vector2.zero);
        anim.SetFloat("sprint", sprintMod);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hurt") && hitCooldown <= 0)
        {
            hitCooldown = 1;
            hitAnim.SetTrigger("Hit");
            health.TakeDamage(1, transform.position);
            healthSlider.value = health.currentHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bed")) { inBed = true; plantText.SetActive(true); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bed")) { inBed = false; plantText.SetActive(false); }
    }

    public void ResetMe()
    {
        transform.position = startPos.position;
        health.currentHealth = health.maxHealth;
        healthSlider.value = health.currentHealth;
    }
}
