using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class PlayerWeapon : MonoBehaviour
{
    public Animator playerAnim;
    public SpriteRenderer playerRenderer;
    public Rigidbody2D playerRb;
    public AudioSource shoveSource, swingSource;

    public Camera cam;
    public SpriteRenderer weaponSr;
    public Transform weaponObject, playerObject, weaponGoal;
    Vector2 defaultOffset;
    public BoxCollider2D weaponCollider, shoveCollider;

    public float shoveSpeed, shoveCooldown;
    float shoveCooldownTimer;

    public TrailRenderer tr;

    public float attackLength, attackOffset;

    float defaultY;
    public float bobOffset;
    public Sprite[] standSprites;

    float lastAngle;
    int lastDir;

    private void Start()
    {
        defaultOffset = weaponGoal.transform.localPosition;
        defaultY = transform.localPosition.y;
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z += 10;
        Vector2 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (transform.position.y < weaponObject.position.y) { weaponSr.sortingOrder = playerRenderer.sortingOrder-1; }
        else { weaponSr.sortingOrder = playerRenderer.sortingOrder+1; }

        playerAnim.SetInteger("dir", GetDirection());

        bool standing = false;
        foreach (Sprite sprite in standSprites) { if (playerRenderer.sprite == sprite) { standing = true; } }
        if (standing) transform.localPosition = new(0, defaultY);
        else transform.localPosition = new(0, defaultY - bobOffset);

        if (shoveCooldownTimer > 0) { shoveCooldownTimer -= Time.fixedDeltaTime; }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponGoal.transform.localPosition = new(defaultOffset.x + attackOffset, defaultOffset.y);
            CancelInvoke("ResetAttack");
            Invoke("ResetAttack", attackLength);
            weaponCollider.enabled = true;
        }

        if (Input.GetMouseButtonDown(1) && shoveCooldownTimer <= 0)
        {
            shoveSource.pitch = Random.Range(0.9f, 1.1f);
            shoveSource.Play();
            shoveCooldownTimer = shoveCooldown;
            weaponGoal.transform.localPosition = new(defaultOffset.x + attackOffset + 0.3f, defaultOffset.y);
            CancelInvoke("ResetAttack");
            Invoke("ResetAttack", attackLength);
            playerRb.velocity += ((Vector2)transform.position - (Vector2)weaponGoal.position).normalized * shoveSpeed;
            shoveCollider.enabled = true;
            tr.emitting = true;
        }
    }

    private void ResetAttack() { 
        weaponGoal.transform.localPosition =  defaultOffset;
        weaponCollider.enabled = false;
        shoveCollider.enabled = false;
        tr.emitting = false;
    }

    private int GetDirection()
    {
        int currentDir = 0;
        float closestAngle = 360;
        float angle = transform.eulerAngles.z + 90;
        if (Mathf.Abs(Mathf.DeltaAngle(lastAngle, angle)) < 3) return lastDir;

        int sum = 45;
        for (int i = 0; Mathf.Abs(i) < 360; i += sum)
        {
            if (Mathf.Abs(Mathf.DeltaAngle(-angle, i)) < closestAngle)
            {
                closestAngle = Mathf.Abs(Mathf.DeltaAngle(-angle, i));
                currentDir = Mathf.Abs(i) / 45;
            }
        }

        playerRenderer.flipX = currentDir <= 4;
        if (currentDir == 7) currentDir = 1;
        if (currentDir == 6) currentDir = 2;
        if (currentDir == 5) currentDir = 3;

        lastAngle = angle;
        lastDir = currentDir;

        return currentDir;
    }
}
