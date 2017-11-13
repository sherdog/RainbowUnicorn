using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCatcher : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile laser = col.gameObject.GetComponent<Projectile>();

		if (laser) {
			if (
				laser.projectileType == Projectile.ProjectileType.PLAYER_LASER || laser.projectileType == Projectile.ProjectileType.ZOMBIE_LASER) {
				Debug.Log ("removing " + laser.projectileType);
				Destroy (col.gameObject);
			}
		}
    }
}
