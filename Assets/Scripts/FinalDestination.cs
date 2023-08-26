using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDestination : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sub"))
        {
            PointMaster.Instance.Win();
        }
    }
}
