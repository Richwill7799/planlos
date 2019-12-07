using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject goal;
    public float speed;

    Rigidbody2D rigidbody2D;
    Rigidbody2D rigidbody2Dgoal;
    Vector2 direction;
    Vector2 goalPos;
    Vector2 position;
    float distance;
    IcePainter icebabyyyyyyy;

    private void Awake()
    {
        icebabyyyyyyy = FindObjectOfType<IcePainter>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2Dgoal = goal.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        distance = Vector2.Distance(position, goalPos);
    }

    private void Update()
    {
        
        //if penguin has reached goal, destroy and reduce hp
        /*if(distance <= 0)
        {
            Destroy(gameObject);
        }*/

        //set movement direction
        goalPos = rigidbody2Dgoal.position;
        position = rigidbody2D.position;
        distance = Vector2.Distance(position, goalPos);

        direction = (goalPos - position) / distance;
        Debug.Log("Direction: " + direction);

        //move penguin
        position += Time.deltaTime * direction * speed;
        rigidbody2D.MovePosition(position);
        if(icebabyyyyyyy.IsHole(position))
            Destroy(gameObject);
    }
    

}
