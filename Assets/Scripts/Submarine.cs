using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Submarine : MonoBehaviour
{
    [SerializeField] private Diver diverScript;
    [SerializeField] private GameObject visibility;
    [SerializeField] private GameObject visibleSub;
    [SerializeField] private SubLight subLight;

    private Camera cam;
    private Rigidbody2D rb;
    private Animator animator;

    public Vector2 dir;
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
            {
                visibleSub.transform.eulerAngles = new Vector3(0, 180, 0);

            }
            else if (dir.x > 0)
            {
                visibleSub.transform.eulerAngles = new Vector3(0, 0, 0);

            }

            if (Input.GetKeyDown(KeyCode.E) && !justSwapped)
            {
                justSwapped = true;
                isSwimming = true;
                diverScript.gameObject.transform.position = transform.position;
                diverScript.spriteRenderer.enabled = true;
                diverScript.SetDiverCamera();
                diverScript.Drowning();
                Invoke(nameof(ResetJustSwappedBool), 0.3f);
            }
        }
    }

    private void ClampVelocity()
    {
        dir.x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        dir.y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Died();
        }
    }

    public void Died()
    {
        isDead = true;
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<Collider2D>().isTrigger = true;
        animator.SetTrigger("Implosion");
        SoundManager.PlaySound(SoundManager.Sound.Implosion);
        GameObject.Find("FadeInOutCanvas").GetComponent<FadeInOutScript>().FadeOut();
    }

    public void SetSubCamera()
    {
        visibility.transform.parent = transform;
        visibility.transform.localPosition = new Vector3(0, 0, 0);

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

            float newSize = Mathf.Lerp(1f, 20, t);

            cam.orthographicSize = newSize;

            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        cam.orthographicSize = 20;
    }

    private void ResetJustSwappedBool()
    {
        justSwapped = false;
    }
}
