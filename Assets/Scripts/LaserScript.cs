using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	public SpriteRenderer laser;
	public SpriteRenderer glow;
	
	public SpriteRenderer lightStart;
	public SpriteRenderer lightMiddle;
	
	public SpriteRenderer start;
	public SpriteRenderer startInner;
	public SpriteRenderer startOuter;
	
	public Transform overlaySpawn;
	
	public Animator laserAnim;
	
	public GameObject overlayPrefab;
	
	private bool spawned=false;

	public void Spawned(){

		laser.gameObject.SetActive(true);
		glow.gameObject.SetActive(true);

		lightStart.gameObject.SetActive(true);
		lightMiddle.gameObject.SetActive(true);
		
		spawned = true;

		laserAnim.SetBool("spawned", true);
	}
	
	void Update(){

		if(spawned){
			float randomOpacity = Random.Range (0.2f, 0.3f);
            //get current color

            if (lightStart && lightMiddle)
            {
                float colorR = lightStart.GetComponent<SpriteRenderer>().color.r;
                float colorB = lightStart.GetComponent<SpriteRenderer>().color.b;
                float colorG = lightStart.GetComponent<SpriteRenderer>().color.b;

                lightStart.color = new Color(colorR, colorB, colorG, randomOpacity);

                if (lightMiddle)
                    lightMiddle.color = new Color(colorR, colorB, colorG, randomOpacity);
            }
            
			
            if(overlayPrefab)
            {
              GameObject newOverlay = Instantiate(overlayPrefab, overlaySpawn.position, Quaternion.identity) as GameObject;
              newOverlay.transform.SetParent(gameObject.transform);
            }
			
		}
	}
}
