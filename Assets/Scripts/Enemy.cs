using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed, accel;
    public Transform player;
    public float range;
    Vector2 goal;
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
            goal = direction.normalized * speed;
        }
        else
        {
            goal = Vector2.zero;
        }

        rb.velocity = Vector2.MoveTowards(rb.velocity, goal, accel * Time.fixedDeltaTime);
    }
}
