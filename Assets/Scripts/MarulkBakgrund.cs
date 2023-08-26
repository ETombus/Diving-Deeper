using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarulkBakgrund : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool movingRight = true;
    private float removeTimer = 0;

    private void Start()
    {
        removeTimer = Random.Range(30, 60);
        InvokeRepeating(nameof(FadeOut), 40, removeTimer);
    }

    void Update()
    {
        if (movingRight)
            transform.Translate(Vector2.right * 1.5f * Time.deltaTime);

        if (!movingRight)
            transform.Translate(-Vector2.right * 1.5f * Time.deltaTime);
    }

    private void FadeOut()
    {
        animator.SetTrigger("Fade");
    }

    public void ResetPosition()
    {
        Vector2 subPos = GameObject.Find("Submarine").transform.position;

        removeTimer = Random.Range(30, 60);
        movingRight = !movingRight;
        float yPos = Mathf.Clamp(subPos.y, -120f, -20f);

        if (movingRight)
        {
            transform.position = new Vector2(subPos.x - 30, yPos);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (!movingRight)
        {
            transform.position = new Vector2(subPos.x + 30, yPos);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}