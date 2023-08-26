using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sub"))
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Sub"))
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
}
