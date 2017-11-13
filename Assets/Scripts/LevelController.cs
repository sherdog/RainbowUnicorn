using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoBehaviour {

	private int _currentLevel = 1;
	private int _enemiesPerRound;
	private int _currentSpawnedEnemies;

	private int _zombieChangeDirection = 7;
	private int _changeDirectionCount = 0;

	private bool _isSpawning = false;

	private float nextSpawn = 0.0f;

	private float spawnInterval = 5.0f;
	private float _minSpawnTime = 3f;
	private float _maxSpawnTime = 8f;

	public GameObject zombiePrefab;
	public AudioClip roundChangeSoundEfx;
	public GameObject player;
	public GameObject roundText;

	private float minY;
	private float maxY;
	private float maxX;

	private List<GameObject> zombies;

	void Start()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
		Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ScreenToViewportPoint (new Vector3 (1, 1, distance));

		maxX = rightMost.x;
		minY = bottomMost.y;
		maxY = topMost.y;

		nextSpawn = Time.time + spawnInterval;
		LoadLevel ();
	}

	public void LoadLevel()
	{
		Debug.Log ("Loading Level " + _currentLevel);

		zombies = new List<GameObject> ();

		_currentSpawnedEnemies = 0;
		_enemiesPerRound = _currentLevel * 10;

		//Play scary intro music.
		if (roundChangeSoundEfx) {
			AudioSource.PlayClipAtPoint(roundChangeSoundEfx, transform.position);
		}

		Invoke ("ShowRoundtext", 2f);

		//invoke for X seconds to start
		Invoke("LevelStart", 4f);
	}

	void ShowRoundtext()
	{
		//Play Round Intro Preab
		roundText.SetActive(true);
	}

	void SetRoundText()
	{
		roundText.GetComponent<Text> ().text = "ROUND " + _currentLevel;
	}

	private void LevelStart()
	{
		roundText.SetActive (false);
		GameState.RoundStarted = true;
	}

	void addSpawnToQueue()
	{

	}

	void SpawnEnemy()
	{
		int zombieSpawnCount = _currentLevel * (Random.Range (1, 5));

		for (int i = 0; i < zombieSpawnCount; i++) {
			Invoke ("AddZombie", Random.Range (0.4f, 0.9f));
		}
	}

	void AddZombie()
	{
		Vector3 zomPos = new Vector3 (maxX + 10, Random.Range (minY, maxY), transform.position.z);
		GameObject enemy = Instantiate (zombiePrefab, zomPos, Quaternion.identity) as GameObject;
		zombies.Add (enemy);

		_currentSpawnedEnemies++;
	}

	void Update()
	{
		if (!GameState.RoundStarted)
			return;

		if (maxSpawned ()) {
			LevelComplete ();
		}

		if(Time.time > nextSpawn && !maxSpawned()) {
			nextSpawn = Time.time + spawnInterval;
			SpawnEnemy();
		}


		//Get players position.. so the zombies can walk towards player
		//we won't change zombies direction each frame.. we want a delay of a direction change.
		Vector3 playerPos = player.transform.position;

		if (_changeDirectionCount == _zombieChangeDirection) {


			//reset
			_changeDirectionCount = 0;
		}

		foreach(GameObject zombie in zombies)
		{
			if (zombie) {
				float speed = zombie.GetComponent<Enemy> ().walkingSpeed;
				zombie.transform.position = new Vector3 (
					zombie.transform.position.x + Time.deltaTime * speed,
					zombie.transform.position.y,
					zombie.transform.position.z);
			}
		}

		_changeDirectionCount++;

	}

	private bool maxSpawned()
	{
		//do some sort of logic to determine in the player kills all of the zombies in the current round;
		if (_currentSpawnedEnemies >= _enemiesPerRound)
			return true;
		
		return false;
	}

	private void LevelComplete()
	{
		GameState.RoundStarted = false;
		_currentLevel++;
		SetRoundText ();
		Invoke ("LoadLevel", 5f);
	}

	private void OnGameEnd()
	{

	}
}
