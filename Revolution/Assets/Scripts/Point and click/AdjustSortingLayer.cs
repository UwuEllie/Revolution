using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSortingLayer : MonoBehaviour
{
    private SpriteRenderer spriteRend; 

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sortingOrder = (int)transform.position.y * - 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
