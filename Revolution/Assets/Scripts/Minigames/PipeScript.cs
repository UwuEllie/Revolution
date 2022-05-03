using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    public float[] correctRotation;
    public bool isPlaced = false;

    int PossibleRots = 1;
    MainManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<MainManager>();
    }

    private void Start()
    {
        PossibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (PossibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaced = true;
                gameManager.PipeCorrectMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isPlaced = true;
                gameManager.PipeCorrectMove();
            }
        }
        
        
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));

        if (PossibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1] && isPlaced == false)
            {
                isPlaced = true;
                gameManager.PipeCorrectMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.PipeWrongMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                gameManager.PipeCorrectMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.PipeWrongMove();
            }
        }
        
    }
}
