using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSpot : MonoBehaviour
{
    public AudioClip transitionToBelow, transitionToAbove;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))          
        {
            if (collision.transform.position.y > transform.position.y)
                AmbienceManager.instance.SwapTrack(transitionToAbove);
            else
                AmbienceManager.instance.SwapTrack(transitionToBelow);
        }
    }
}
