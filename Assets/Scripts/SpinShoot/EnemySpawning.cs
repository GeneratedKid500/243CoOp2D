using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    GameObject[] waypoints;

    GameObject player;

    List<GameObject> enemies;
    public int maxEnemies = 10;

    public float spawnTimer = 2;
    float timer;

    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        player = GameObject.FindGameObjectWithTag("Player");

        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count >= maxEnemies)
        {
            timer = 0;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnTimer && enemies.Count < maxEnemies)
        {
            Vector3 i = waypoints[Random.Range(0, waypoints.Length)].transform.position;
            GameObject enemy = Instantiate(enemyPrefab, waypoints[Random.Range(0, waypoints.Length)].transform);
            enemies.Add(enemy);
            timer = 0;
        }
    }

    public void Kill(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
