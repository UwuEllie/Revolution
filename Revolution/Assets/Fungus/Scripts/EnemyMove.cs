using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Vector2 followSpot;
    public float speed;
    private Vector3 newPos;

    private NavMeshAgent agent;
    public Animator anim;
    private SpriteRenderer spriteRend;
    private Vector2 stuckDistanceCheck;

    //test
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private float timer;

    void Start()
    {
        followSpot = transform.position;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //test
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        AdjustSortingLayer();

        if (timer >= wanderTimer)
        {
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float distance = Vector2.Distance(transform.position, newPos);

        if (Vector2.Distance(stuckDistanceCheck, transform.position) == 0)
        {
            anim.SetFloat("Distance", 0f);
            return;
        }

        anim.SetFloat("Distance", distance);

        if (distance > 0.01)
        {
            Vector3 direction = transform.position - new Vector3(newPos.x, newPos.y, transform.position.z);
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            anim.SetFloat("Angle", angle);
            stuckDistanceCheck = transform.position;
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void AdjustSortingLayer()
    {
        spriteRend.sortingOrder = (int)transform.position.y * -100; // 50 is pixels per unit number 
    }
}
