using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Submarine : MonoBehaviour
{
    [SerializeField] private Diver diverScript;
    [SerializeField] private GameObject visibility;

    private Camera cam;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 dir;
    private float speed = 5;
    private float maxVelocity = 15f;

    public bool isDead;
    public bool isSwimming;
    public bool justSwapped;

    private void Start()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isDead && !isSwimming)
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            dir.y = Input.GetAxisRaw("Vertical");

            rb.velocity += dir * speed * Time.deltaTime;
            ClampVelocity();

            if (dir.x < 0)
                spriteRenderer.flipX = false;
            else if (dir.x > 0)
                spriteRenderer.flipX = true;

            if (Input.GetKeyDown(KeyCode.E) && !justSwapped)
            {
                justSwapped = true;
                isSwimming = true;
                diverScript.gameObject.transform.position = transform.position;
                diverScript.spriteRenderer.enabled = true;
                diverScript.SetDiverCamera();

                Invoke(nameof(ResetJustSwappedBool), 1f);
            }

        }
    }

    private void ClampVelocity()
    {
        dir.x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        dir.y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = dir;
    }

    public void SetSubCamera()
    {
        visibility.transform.parent = transform;

        cam.transform.parent = transform;
        cam.transform.localPosition = new Vector3(0, 0, -10);
        StartCoroutine(LerpCameraSize());
    }

    private IEnumerator LerpCameraSize()
    {
        float elapsedTime = 0f;
        float lerpDuration = 0.3f;

        while (elapsedTime < lerpDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            float newSize = Mathf.Lerp(0.5f, 5, t);

            cam.orthographicSize = newSize;

            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        cam.orthographicSize = 5;
    }

    private void ResetJustSwappedBool()
    {
        justSwapped = false;
    }
}
