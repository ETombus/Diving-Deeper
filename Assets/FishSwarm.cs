using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwarm : MonoBehaviour
{
    private bool movingRight = true;
    private float removeTimer = 0;

    private void Start()
    {
        removeTimer = Random.Range(15, 30);
        InvokeRepeating(nameof(ResetPosition), 10, removeTimer);
    }

    void Update()
    {
        if (movingRight)
            transform.Translate(Vector2.right * 3f * Time.deltaTime);

        if (!movingRight)
            transform.Translate(-Vector2.right * 3f * Time.deltaTime);
    }

    public void ResetPosition()
    {
        Vector2 subPos = GameObject.Find("Submarine").transform.position;

        removeTimer = Random.Range(10, 20);
        movingRight = !movingRight;
        float yPos = Mathf.Clamp(subPos.y, -120f, -5f);
        Debug.Log(yPos);

        if (movingRight)
        {
            transform.position = new Vector2(subPos.x - 6, yPos);
            foreach(SpriteRenderer fishSR in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                fishSR.flipX = false;
            }
        }
        if (!movingRight)
        {
            transform.position = new Vector2(subPos.x + 6, yPos);
            foreach (SpriteRenderer fishSR in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                fishSR.flipX = true;
            }

        }
    }
}