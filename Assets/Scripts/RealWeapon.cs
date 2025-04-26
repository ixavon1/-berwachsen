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
}
