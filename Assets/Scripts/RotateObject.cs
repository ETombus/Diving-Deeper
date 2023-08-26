using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Speed of rotation in degrees per second
    public float targetRotation = 90.0f; // Target rotation angle in degrees
    public RotationDirection rotationDirection = RotationDirection.Clockwise; // Select the rotation direction in the Inspector

    private Quaternion startRotation;
    private Quaternion targetQuaternion;
    private bool isRotating = false;

    public enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }

    private void Start()
    {
        float targetAngle = (rotationDirection == RotationDirection.Clockwise) ? targetRotation : -targetRotation;
        targetQuaternion = Quaternion.Euler(0f, 0f, targetAngle);
        startRotation = transform.rotation;
    }

    private void Update()
    {
        RotateTowardsTarget();
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation()
    {
        isRotating = false;
    }

    private void RotateTowardsTarget()
    {
        // Calculate the amount of rotation for this frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Rotate towards the target angle or the start rotation if stopping
        Quaternion targetRot = isRotating ? targetQuaternion : startRotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationAmount);

        // Check if the target rotation is reached
        if (Quaternion.Angle(transform.rotation, targetRot) <= 0.01f)
        {
            // Snap to the exact target rotation or the start rotation if stopping
            transform.rotation = targetRot;
        }
    }
}
