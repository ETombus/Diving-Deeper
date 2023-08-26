using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutTrigger : MonoBehaviour
{
    Camera cam;

    public float camZoom = 40;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sub"))
        {
            StopAllCoroutines();
            StartCoroutine(ZoomOut());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomBack());
    }

    private IEnumerator ZoomOut()
    {
        float elapsedTime = 0f;
        float lerpDuration = 5f;
        float currentCamSize = cam.orthographicSize;

        while (elapsedTime < lerpDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            float newSize = Mathf.Lerp(currentCamSize, camZoom, t);

            cam.orthographicSize = newSize;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cam.orthographicSize = camZoom;
    }
    private IEnumerator ZoomBack()
    {
        float elapsedTime = 0f;
        float lerpDuration = 5f;
        float currentCamSize = cam.orthographicSize;

        while (elapsedTime < lerpDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            float newSize = Mathf.Lerp(currentCamSize, 15f, t);

            cam.orthographicSize = newSize;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cam.orthographicSize = 15f;
    }
}
