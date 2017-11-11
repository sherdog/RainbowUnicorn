using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    float yMin;
    float yMax;

    public float maxHealth = 1400f;
    public float currentHealth = 0f;
    public float speed;
    public float projectileSpeed = 1.0f;
    public GameObject laser;
    public float fireRate = 0.2f;
    public float padding = 0.7f;
    public Slider healthBar;
    public AudioClip shootSound;
    bool isTriggered = false;
    public GameObject superLaser;
    bool superLaserActive = false;
    GameObject sl;

    void Start ()
    {
        currentHealth = maxHealth;

        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));
        Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distance));

        yMin = bottomMost.y + padding;
        yMax = topMost.y - padding;

        UpdateHealthBar();
    }
	
    void UpdateHealthBar()
    {
        healthBar.value = CalculateHealth();
    }

	void Update ()
    {

        if (currentHealth < (maxHealth / 2))
            isTriggered = true;
            
        if (Input.GetKey(KeyCode.DownArrow))
            transform.position += Vector3.down * (speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.UpArrow))
            transform.position += Vector3.up * (speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            InvokeRepeating("Shoot", 0.000001f, fireRate);

        if (Input.GetKeyUp(KeyCode.Space))
            CancelInvoke();

        if(Input.GetKeyDown(KeyCode.B))
        {
            if(isTriggered)
            {
                isTriggered = false;
                destorySuperLaser();
            }
            else
            {
                isTriggered = true;
                activateSuperLaser();
            }
        }
            
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        healthBar.value = CalculateHealth();
    }

    void destorySuperLaser()
    {
        if(sl)
            Destroy(sl);
    }
    void activateSuperLaser()
    {
        superLaserActive = true;

        Vector3 offset = new Vector3(1, 0);
        sl = Instantiate(superLaser, transform.position + offset, Quaternion.identity) as GameObject;

        sl.transform.parent = transform;
        sl.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile laser = col.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            currentHealth -= laser.damage;
            laser.Hit();

            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                BoomImDead();
            }
        }
    }

    float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    void BoomImDead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    void Shoot()
    {
        
        if (GameState.RoundStarted)
        {
            Vector3 offset = new Vector3(1, 0);
            GameObject beam = Instantiate(laser, transform.position + offset, Quaternion.identity) as GameObject;
            beam.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0);

            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }
}
