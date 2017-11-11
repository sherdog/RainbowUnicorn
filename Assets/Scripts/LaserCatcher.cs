using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCatcher : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile laser = col.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            Destroy(col.gameObject);
        }
    }
}
