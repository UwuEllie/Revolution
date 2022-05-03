
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{
    public int curCombinationValue;
    public int correctCombinationNumber;
    public Sprite[] combinationSprites;

    public GameObject combinationHolder;
    private SpriteRenderer rend;
    private CombinationCorrectCheck combinationCheck;

    void Start()
    {
        combinationCheck = combinationHolder.gameObject.GetComponent<CombinationCorrectCheck>();
        rend = GetComponent<SpriteRenderer>();
        if (curCombinationValue == correctCombinationNumber) { combinationCheck.correctcombinations += 1; }
    }

    private void OnMouseDown()
    {
        if (curCombinationValue == correctCombinationNumber) { combinationCheck.correctcombinations -= 1; }

        curCombinationValue += 1;
        if (curCombinationValue > combinationSprites.Length - 1) { curCombinationValue = 0; }
        rend.sprite = combinationSprites[curCombinationValue];

        if (curCombinationValue == correctCombinationNumber) { combinationCheck.correctcombinations += 1; }
    }
}
