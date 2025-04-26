using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public Transform player;
    bool turned;
    float rotating;
    float goal;

    public Sprite[] sprites;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) < 0.5f)
        {
            if (!turned)
            {
                turned = true;
                rotating = 0.25f;
                goal = Random.Range(-30, 30);
            }
        }
        else { turned = false; }

        if (rotating > 0) 
        {
            rotating -= Time.fixedDeltaTime;
            transform.eulerAngles = new(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, goal, Time.fixedDeltaTime * 120));
        }
    }
}
