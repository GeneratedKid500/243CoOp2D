using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    GameObject[] waypoints;

    public GameObject player;

    public Text score;
    int playerScore = 0;

    List<GameObject> enemies;
    public int maxEnemies = 10;

    AudioSource audioSource;
    public AudioClip[] deathSounds;

    public float spawnTimer = 2;
    float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        waypoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        player = GameObject.FindGameObjectWithTag("player");

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

        playerScore += 10;
        score.text = "Score: " + playerScore.ToString();

        audioSource.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length - 1)]);

        Destroy(enemy, 0.5f);
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
