using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject pipeHolder;
    public GameObject[] pipes;

    public int totalPipes = 0;

    int correctedPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = pipeHolder.transform.childCount;
        pipes = new GameObject[totalPipes];

        for (int i = 0; i < totalPipes; i++)
        {
            pipes[i] = pipeHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PipeCorrectMove()
    {
        correctedPipes += 1;
        if (correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
        }
    }

    public void PipeWrongMove()
    {
        correctedPipes -= 1; 
    }
}
