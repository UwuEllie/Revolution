using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Books : MonoBehaviour
{
    public bool IsVisible;
    public int positionInPuzzle;
    public int correctPlace; 

    public bool currentlySelected;
    BookSwap bookSwap;
    public GameObject BookSwapManager;
    public Vector2 movePosition;
    Vector2 newmovepos;

    Vector2 originalPos;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        bookSwap = BookSwapManager.gameObject.GetComponent<BookSwap>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlySelected && Vector2.Distance(transform.position, newmovepos) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, newmovepos, speed);
        }

    }

    private void OnMouseDown()
    {
        if (IsVisible)
        {
            if (bookSwap.book1 == null)
            {
                currentlySelected = true;
                bookSwap.book1 = gameObject;
                newmovepos = new Vector2(transform.position.x, transform.position.y + 0.5f);
            }
            else
            {
                if (bookSwap.book1 == this.gameObject)
                {
                    currentlySelected = false;
                    resetBook();
                    bookSwap.book1 = null;
                    return;
                }

                bookSwap.book2 = gameObject;
                bookSwap.swapBooks();
            }
        }
        
    }

    public void resetBook()
    {
        currentlySelected = false;
        transform.position = new Vector2(transform.position.x, originalPos.y);
    }
}
