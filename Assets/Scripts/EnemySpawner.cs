using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float width = 8f;
    public float height = 10f;
    public float padding = 0f;
    float direction = 1f;
    public float speed = 1.0f;
    public float spawnDelay = 0.5f;

    float minY;
    float maxY;

    bool gameStarted = false;

    public GameObject enemyPrefab;

	public void StartSpawning(int totalEnemies, int currentlevel)
	{
		//Current level.. higher the level higher the possible spawn count;

		//just start spawning eneies.
		//
	}

	void Start () {

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));

        minY = bottomMost.y + height / 2;
        maxY = topMost.y - height / 2 ;

        Invoke("StartGame", 5.0f);

        SpawnUntilFull();
    }

    void Update()
    {
        if (gameStarted)
        {
            if (transform.position.y >= maxY)
                direction = 1.0f;
            else if (transform.position.y <= minY)
                direction = -1.0f;

            transform.position += Vector3.down * ((direction * speed) * Time.deltaTime);

            if (AllEnemiesAllDead())
            {
                SpawnUntilFull();
            }
        }
        
    }

    void StartGame()
    {
        GameState.RoundStarted = true;
        gameStarted = true;
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePos = NextFreePosition();

        if(freePos)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePos;
        }

        if(NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    bool AllEnemiesAllDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
                return false;
        }

        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
                return childPositionGameObject;
        }

        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}
