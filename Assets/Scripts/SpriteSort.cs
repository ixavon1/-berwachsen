using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSort : MonoBehaviour
{
    public SpriteRenderer sr;
    public int offset;
    public bool useOnUpdate;

    private void Start()
    {
        if (!sr) { sr = GetComponent<SpriteRenderer>(); }
        sr.sortingLayerName = "Active";
        Sort();
        if (!useOnUpdate) { this.enabled = false; }
    }

    private void FixedUpdate()
    {
        Sort();
    }

    public void Sort()
    {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * -100 + offset);
    }
}
