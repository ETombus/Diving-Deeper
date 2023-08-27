using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubLight : MonoBehaviour
{
    private Submarine subScript;

    private void Start()
    {
        subScript = GetComponentInParent<Submarine>();
    }

    private void Update()
    {
        if (!subScript.isDead && !subScript.isSwimming)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 direction = mousePos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 50;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
