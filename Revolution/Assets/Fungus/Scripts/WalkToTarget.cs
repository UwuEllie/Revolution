using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Fungus;

public class WalkToTarget : MonoBehaviour
{
    public Vector2 followSpot;
    public float speed;
    public float perspectiveScale;
    public float scaleRatio;

    private NavMeshAgent agent;
    public Animator anim;

    private Vector2 stuckDistanceCheck;
    private SpriteRenderer spriteRend;
    public bool InDialogue;
    public bool cutSceneInProgress;

    private Verb verb;
    public bool inCloset;

    // Start is called before the first frame update
    void Start()
    {
        followSpot = transform.position;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        verb = FindObjectOfType<Verb>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InDialogue)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)) // left clicking
            {
                followSpot = new Vector2(mousePosition.x, mousePosition.y);
            }

            agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));
            UpdateAnimation();
        }
        AdjustSortingLayer();

        //transform.position = Vector2.MoveTowards(transform.position, followSpot, speed * Time.deltaTime);
        //adjustPerspective();
    }

    private void adjustPerspective()
    {
        Vector3 scale = transform.localScale;
        scale.x = perspectiveScale * (scaleRatio - transform.position.y);
        scale.y = perspectiveScale * (scaleRatio - transform.position.y);
        transform.localScale = scale; 
    }

    private void UpdateAnimation()
    {
        float distance = Vector2.Distance(transform.position, followSpot);

        if (Vector2.Distance(stuckDistanceCheck, transform.position) == 0)
        {
            anim.SetFloat("Distance", 0f);
            return;
        }

        anim.SetFloat("Distance", distance);

        if (distance > 0.01)
        {
            Vector3 direction = transform.position - new Vector3(followSpot.x, followSpot.y, transform.position.z);
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            anim.SetFloat("Angle", angle);
            stuckDistanceCheck = transform.position;
        }
        
    }

    private void AdjustSortingLayer()
    {
        spriteRend.sortingOrder = (int)transform.position.y * - 100; // 50 is pixels per unit number 
    }

    public void ExitDialogue()
    {
        InDialogue = false;
        cutSceneInProgress = false;

        verb.verb = Verb.Action.walk;
        verb.UpdateVerbTextBox(null);
        verb.gameObject.SetActive(true);
    }

    public void EnterDialogue()
    {
        InDialogue = true;
        cutSceneInProgress = true;

        verb.verb = Verb.Action.walk;
        verb.UpdateVerbTextBox(null);
        verb.gameObject.SetActive(false);
    }

    public void enterCloset()
    {
        inCloset = !inCloset;
    }
}
