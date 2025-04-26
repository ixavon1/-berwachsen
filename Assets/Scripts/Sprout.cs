using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    public float timer;
    public GameObject toSpawn;
    public Sprite[] sprites;

    private void Start()
    {
        Invoke("Grow", timer);
        GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1.3f);
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new(0, 0, Random.Range(-15, 15));
    }

    private void Grow()
    {
        GameObject go = Instantiate(toSpawn, transform.position, Quaternion.identity);
        go.SetActive(true);
        Destroy(gameObject);
    }
}
