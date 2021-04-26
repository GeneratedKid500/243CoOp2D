using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int maxHP;
    float currentHP;

    public float moveSpeed = 4;

    EnemySpawning enemySpawning;
    GameObject player;
    PlayerManager pM;

    Rigidbody2D rb;

    void Awake()
    {
        currentHP = maxHP;
        enemySpawning = GetComponentInParent<EnemySpawning>();

        player = enemySpawning.GetPlayer();
        pM = player.GetComponent<PlayerManager>();

        transform.parent = null;

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
    }

    void TakeDamage(float damage)
    {
        if (currentHP <= 0)
            enemySpawning.Kill(this.gameObject);
        else
        {
            currentHP -= damage;
            if (currentHP <= 0)
                enemySpawning.Kill(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        switch (collision.tag) 
        {
            case "Bullet":
                TakeDamage(10);
                Destroy(collision.gameObject);
                break;

            case "Enemy":
                break;

            case "Player":
                pM.TakeDamage(10);
                Destroy(this.gameObject);
                break;

            case "Shield":
                //Bounce off??S
                break;
        }

    }
}
