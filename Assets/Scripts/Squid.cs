using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;


public class Squid : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Sprite[] sprites;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 currentPos;

    private int nextPoint;

    private float moveTime = 2f;
    private float waitTime = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentPos = transform.position;
        float random = UnityEngine.Random.Range(0f, 3f);
        StartCoroutine(Boost());
    }

    private IEnumerator Boost()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 3f));

        while (true)
        {
            Vector2 directionToTarget = (Vector2)waypoints[nextPoint].position - currentPos;
            float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle - 90);

            float t = 0f;

            while (t < moveTime)
            {


                t += Time.deltaTime;

                float easedT = EasingFunction(t / moveTime);
                Vector2 newPos = Vector2.Lerp(currentPos, waypoints[nextPoint].position, easedT);
                rb.MovePosition(newPos);

                spriteRenderer.sprite = sprites[1];

                yield return null;
            }

            spriteRenderer.sprite = sprites[0];
            currentPos = waypoints[nextPoint].position;
            nextPoint++;
            if (nextPoint == waypoints.Length)
            {
                Array.Reverse(waypoints);
                foreach (var waypoint in waypoints)
                    Debug.Log(waypoint.ToString());
                nextPoint = 1;
            }


            yield return new WaitForSeconds(waitTime);
        }
    }

    private float EasingFunction(float t)
    {
        return 1f - Mathf.Pow(1f - t, 5f);
    }
}
