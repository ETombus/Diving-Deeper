using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarObj : MonoBehaviour
{
    public GameObject obj;
    public GameObject radarObj;

    public float smallDist;
    public float mediumDist;

    private void Update()
    {
        Vector2 dir = obj.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        if (Vector2.Distance(transform.position, obj.transform.position) < smallDist)
            radarObj.transform.localScale = new Vector2(0.3f, 0.3f);
        else if (Vector2.Distance(transform.position, obj.transform.position) < mediumDist)
            radarObj.transform.localScale = new Vector2(0.2f, 0.2f);
        else
            radarObj.transform.localScale = new Vector2(0.1f, 0.1f);
    }
}
