using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoveCollider : MonoBehaviour
{
    public Transform player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(-1, player.position);
        }
    }
}
