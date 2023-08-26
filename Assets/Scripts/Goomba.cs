using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float speed;

    public Transform leftPoint;
    public Transform rightPoint;

    private Vector2 dir;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = transform.right;
    }

    private void Update()
    {
        rb.velocity = dir * speed;

        if (transform.position.x > rightPoint.position.x)
            dir = transform.right * -speed;
        else if (transform.position.x < leftPoint.position.x)
            dir = transform.right * speed;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Titan sub moments frfr");
        }
    }
}
