using UnityEngine;
using UnityEngine.Events;

public class ColliderEventTrigger : MonoBehaviour
{
    private string targetTag = "Player"; // The tag to compare with
    public UnityEvent enterEvent; // The event to trigger on enter
    public UnityEvent exitEvent; // The event to trigger on exit

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            enterEvent.Invoke(); // Trigger the enter event
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            exitEvent.Invoke(); // Trigger the exit event
        }
    }
}
