using UnityEngine;

public class ExpandCollider : MonoBehaviour
{
    public float expansionSpeed = 1.0f; // Speed of expansion/contraction in units per second
    public float targetSize = 2.0f; // Target Y-axis size

    public float startExpandDelay = 1.0f; // Delay before expanding starts when at start size

    private bool isExpanding = false;
    private Vector2 startSize;
    private Vector2 targetSizeVector;
    private BoxCollider2D boxCol2D;

    private void Start()
    {
        boxCol2D = GetComponent<BoxCollider2D>();
        startSize = boxCol2D.bounds.size;
        targetSizeVector = new Vector2(startSize.x, targetSize);
    }

    private void Update()
    {
        AdjustColliderSize();
    }

    public void StartExpansion()
    {
        isExpanding = true;
    }

    public void StopExpansion()
    {
        isExpanding = false;
    }

    public void DelayedStartExpansion()
    {
        if (boxCol2D.bounds.size.y < targetSize)
        {
            isExpanding = true;
        }
        else
        {
            Invoke("StartExpansion", startExpandDelay);
        }
    }

    private void AdjustColliderSize()
    {
        float step = expansionSpeed * Time.deltaTime;

        Vector2 targetSize = isExpanding ? targetSizeVector : startSize;
        Vector2 newSize = Vector2.MoveTowards(boxCol2D.bounds.size, targetSize, step);

        // Update the collider's Y-axis size
        boxCol2D.size = newSize;

        // Check if the target size is reached
        if (Vector2.Distance(boxCol2D.bounds.size, targetSize) < 0.01f)
        {
            // Snap to the exact target size
            boxCol2D.size = targetSize;
            isExpanding = false; // Stop the expansion
        }
    }
}
