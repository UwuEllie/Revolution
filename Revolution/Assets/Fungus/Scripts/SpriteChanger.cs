using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public GameObject ObjectHolder;
    private SpriteRenderer rend;
    public Sprite newSprite;

    public bool ShowItem;
    public GameObject ObjectsToShow;

    public void ChangeSprite()
    {
        rend = ObjectHolder.gameObject.GetComponent<SpriteRenderer>();
        rend.sprite = newSprite;

        if (ShowItem) { ObjectsToShow.SetActive(true); }
    }
}
