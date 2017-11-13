using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public enum ProjectileType {PLAYER_LASER, ZOMBIE_LASER, SUPER_LASER};
    public float damage;
	public ProjectileType projectileType = ProjectileType.PLAYER_LASER;

    public void Hit()
    {
		if (projectileType != ProjectileType.SUPER_LASER) {
			Destroy (gameObject);
		}
    }
}
