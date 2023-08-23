using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField] private GameObject bubbles;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 dir;
    private float speed = 5;
    private float maxVelocity = 10f;

    public bool isDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            dir.y = Input.GetAxisRaw("Vertical");

            if (dir.y == 0 && dir.x == 0)
            {
                Vector2 lerpForce = Vector2.Lerp(Vector2.zero, rb.velocity, Time.deltaTime);

                rb.velocity -= lerpForce;
            }
            else
            {
                rb.velocity += dir * speed * Time.deltaTime;
            }

            ClampVelocity();
        }
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(x, y);
    }
}
