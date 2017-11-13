using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float health = 250f;

    
    private ScoreKeeper scoreKeeper;

    public GameObject zombieBullet;
    public float bulletSpeed = 10f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 130;
    public Sprite EnemyHitSprite;
    public AudioClip zombieDieSound;
    public AudioClip zombieHitSound;
    public GameObject bloodParticle;
	public float _walkingSpeed = 0.4f;

	public float walkingSpeed { get { return _walkingSpeed; } }

    void Start()
    {
        scoreKeeper = GameObject.Find("score").GetComponent<ScoreKeeper>();
		_walkingSpeed = -(Random.Range (1.0f, 2.0f));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile laser = col.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            EnemyHit(laser);
        }
    }

    void EnemyHit(Projectile laser)
    {
        health -= laser.damage;
        laser.Hit();
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            KillEnemy();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = EnemyHitSprite;
            AudioSource.PlayClipAtPoint(zombieHitSound, transform.position);
        }
    }

    void Update()
    {
        if ( GameState.RoundStarted)
        {
            float prob = Time.deltaTime * shotsPerSecond;

            if (Random.value < prob)
            {
                Fire();
            }
        }
    }

    void KillEnemy()
    {
        scoreKeeper.UpdateScore(scoreValue);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(zombieDieSound, transform.position);
    }

    void Fire()
    {
        Vector3 startPos = transform.position;
        GameObject bullet = Instantiate(zombieBullet, startPos + new Vector3(-1, 0, 0), Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(-bulletSpeed, 0);
    }
}
