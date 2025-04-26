using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public SpriteRenderer sr, shadowSr, reflectionSr;

    public float speed, accel;
    private float sprintMod;

    Vector2 input;

    public SpriteMask mask;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, input.normalized * speed * sprintMod, accel * Time.fixedDeltaTime * sprintMod);

        anim.SetBool("walking", input != Vector2.zero);
        anim.SetFloat("sprint", sprintMod);
    }
}
