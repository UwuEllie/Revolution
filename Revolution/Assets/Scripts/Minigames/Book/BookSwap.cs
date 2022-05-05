using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSwap : MonoBehaviour
{
    public GameObject book1;
    public GameObject book2;
    Vector2 movePosition;
    public GameObject[] books;

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

   public void swapBooks()
   {
        Vector3 placeholderBookPosititon = book2.transform.position;

        Books book = book1.gameObject.GetComponent<Books>();
        Books book2script = book2.gameObject.GetComponent<Books>();

        book.resetBook();

        book2.transform.position = book1.transform.position;
        book1.transform.position = placeholderBookPosititon;

        int placeholderInt = book2script.positionInPuzzle;
        book2script.positionInPuzzle = book.positionInPuzzle;
        book.positionInPuzzle = placeholderInt;
        
        book1 = null;
        book2 = null;

        checkBooks();
   }

    void checkBooks()
    {
        correctcombinations = 0;

        foreach (GameObject book in books)
        {
            Books bookscript = book.gameObject.GetComponent<Books>();

            if (bookscript.IsVisible && bookscript.positionInPuzzle == bookscript.correctPlace)
            {
                correctcombinations += 1;
            }
            else
            {
                return;
            }
        }

       
    }
}
