using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationCorrectCheck : MonoBehaviour
{
    public int correctcombinations;
    public int totalCombinations; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (correctcombinations == totalCombinations)
        {
            Debug.Log("I love you");
        }
    }
}
