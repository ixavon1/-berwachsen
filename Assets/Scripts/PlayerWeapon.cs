using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer weaponSr;
    public Transform weaponObject, playerObject, weaponGoal;
    Vector2 defaultOffset;

    public float attackLength, attackOffset;

    private void Start()
    {
        defaultOffset = weaponGoal.transform.localPosition;
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z += 10;
        Vector2 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (playerObject.position.y < weaponObject.position.y) { weaponSr.sortingOrder = 1; }
        else { weaponSr.sortingOrder = 3; }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponGoal.transform.localPosition = new(defaultOffset.x + attackOffset, defaultOffset.y);
            CancelInvoke("ResetAttack");
            Invoke("ResetAttack", attackLength);
        }
    }

    private void ResetAttack() { weaponGoal.transform.localPosition =  defaultOffset; }
}
