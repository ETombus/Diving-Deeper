using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Diver : MonoBehaviour
{
    [SerializeField] private Submarine subScript;
    [SerializeField] private GameObject visibleDiver;
    [SerializeField] private GameObject visibility;

    [SerializeField] private List<GameObject> radarSquares = new List<GameObject>();
    [SerializeField] private List<GameObject> radarDots = new List<GameObject>();
    private Dictionary<GameObject, GameObject> radarDic = new Dictionary<GameObject, GameObject>();

    private Camera cam;
    private Rigidbody2D rb;
    private Animator animator;

    public SpriteRenderer spriteRenderer;

    public int points = 0;

    private Vector2 rotation;
    private float maxVelocity = 1.5f;
    private float rotationSpeed = 5f;

    private bool inSubRange;

    void Start()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody2D>();

        animator = visibleDiver.GetComponent<Animator>();
        spriteRenderer = visibleDiver.GetComponent<SpriteRenderer>();

        for (int i = 0; i < radarDots.Count; i++)
        {
            radarDic.Add(radarSquares[i], radarDots[i]);
        }
    }

    void Update()
    {
        if (!subScript.isDead && subScript.isSwimming)
        {
            rotation.x = Input.GetAxisRaw("Horizontal");
            rotation.y = Input.GetAxisRaw("Vertical");

            if (rotation.x != 0 || rotation.y != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotation);
                visibleDiver.transform.rotation = Quaternion.Slerp(visibleDiver.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown("space"))
            {
                Vector2 force = (visibleDiver.transform.rotation * Vector2.up) * 3f;
                rb.AddForce(force, ForceMode2D.Force);
            }

            float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
            float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
            rb.velocity = new Vector2(x, y);


            if (Input.GetKeyDown(KeyCode.E) && inSubRange && !subScript.justSwapped)
            {
                subScript.justSwapped = true;
                subScript.isSwimming = false;
                spriteRenderer.enabled = false;
                subScript.SetSubCamera();
                rb.velocity = Vector3.zero;
                GameObject.Find("FadeInOutCanvas").GetComponent<FadeInOutScript>().IsDrowning(false);
                Invoke(nameof(ResetJustSwappedBool), 0.3f);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            points++;
            Destroy(radarDic[collision.gameObject]);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Sub"))
        {
            inSubRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sub"))
        {
            inSubRange = false;
        }
    }

    public void Drowning()
    {
        GameObject.Find("FadeInOutCanvas").GetComponent<FadeInOutScript>().IsDrowning(true);
    }

    public void SetDiverCamera()
    {
        cam.transform.parent = transform;
        visibility.transform.parent = transform;
        StartCoroutine(LerpCameraSize());
    }

    private IEnumerator LerpCameraSize()
    {
        float elapsedTime = 0f;
        float lerpDuration = 0.3f;

        while (elapsedTime < lerpDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            float newSize = Mathf.Lerp(5, 0.5f, t);

            cam.orthographicSize = newSize;

            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        cam.orthographicSize = 0.5f;
    }



    private void ResetJustSwappedBool()
    {
        subScript.justSwapped = false;
    }
}
