using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public GameObject dad;
    public SpriteRenderer sr;
    public Material flashMat;
    public Rigidbody2D rb;
    Material basicMat;

    public float knockback;

    AudioSource source;
    public AudioClip[] clips;
    public GameObject deathObject;

    public ParticleSystem ps;

    void Start()
    {
        currentHealth = maxHealth;
        if (sr) basicMat = sr.material;
        TryGetComponent<AudioSource>(out source);
    }

    public void TakeDamage(int damage, Vector2 pos)
    {
        currentHealth -= Mathf.Abs(damage);

        if(ps) ps.Play();

        if (sr) { sr.material = flashMat; CancelInvoke("Unflash"); Invoke("Unflash", 0.15f); }
        if (rb) 
        {
            float mult = 1;
            if (damage < 0) mult = 5;
            rb.velocity += (rb.position - pos).normalized * knockback * mult;
        }

        if (clips.Length > 0) { source.pitch += 0.14f; source.clip = clips[Random.Range(0, clips.Length)]; source.Play(); }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathObject)
        {
            GameObject go = Instantiate(deathObject, transform.position, Quaternion.identity);
            go.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            Destroy(go, 3);
        }
        if (dad) Destroy(dad);
        Destroy(gameObject);
    }

    private void Unflash() { sr.material = basicMat; }
}