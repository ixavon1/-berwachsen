using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed, accel;
    public Transform player;
    public float range;

    Vector2 goal;

    public SpriteRenderer sr;
    public Sprite nice, evil;
    public Transform sprite;
    bool playerIn, rotating;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        if (direction.magnitude < range)
        {
            if (!playerIn) 
            {
                playerIn = true;
                rotating = true; 
                CancelInvoke("Rotate"); 
                Invoke("Rotate", 0.25f);
                CancelInvoke("SwitchSprite");
                Invoke("SwitchSprite", 0.125f);
            }
            goal = direction.normalized * speed;
        }
        else
        {
            if (playerIn) 
            {
                playerIn = false; 
                rotating = true; 
                CancelInvoke("Rotate");
                Invoke("Rotate", 0.25f);
                CancelInvoke("SwitchSprite");
                Invoke("SwitchSprite", 0.125f);
            }
            goal = Vector2.zero;
        }

        if (rotating)
        {
            sprite.localEulerAngles += new Vector3(0, 720 * Time.fixedDeltaTime, 0);
        }

        rb.velocity = Vector2.MoveTowards(rb.velocity, goal, accel * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        rotating = false;
        sprite.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void SwitchSprite()
    {
        if (playerIn) sr.sprite = evil;
        else sr.sprite = nice;
    }
}
