using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealWeapon : MonoBehaviour
{
    public float moveSpeed;
    public Transform goal;

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, goal.localPosition, moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(1);
        }
    }
}
