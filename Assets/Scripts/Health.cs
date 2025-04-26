using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

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

    public int id;

    public Player player;
    public GameObject death;
    bool dying;

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
        if (id != -1 && player)
        {
            player.plantAmts[id]++;
            player.plantTexts[id].text = "x" + player.plantAmts[id];
            //player.plantTexts[id].color = Color.white;
        }

        if (dying) return;
        if (death)
        {
            death.GetComponent<Animator>().Play("fadeout");
            death.GetComponent<AudioSource>().Play();
            Invoke("ResetPlayer", 1);
            dying = true;
            return;
        }
        if (deathObject)
        {
            GameObject go = Instantiate(deathObject, transform.position, Quaternion.identity);
            go.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            Destroy(go, 3);
        }
        if (dad) Destroy(dad);
        Destroy(gameObject);
    }

    void ResetPlayer()
    {
        player.ResetMe();
        source.pitch = 1;
    }

    private void Unflash() { sr.material = basicMat; }
}