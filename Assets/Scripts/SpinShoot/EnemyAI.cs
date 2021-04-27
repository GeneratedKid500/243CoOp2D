using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Sprite[] ships;
    int spriteNo;

    public int maxHP;
    float currentHP;

    public float moveSpeed = 4;

    EnemySpawning enemySpawning;
    public RuntimeAnimatorController rtac;
    Animator anim;
    GameObject player;
    PlayerManager pM;

    SpriteRenderer sprRen;

    Rigidbody2D rb;

    void Awake()
    {
        currentHP = maxHP;
        enemySpawning = GetComponentInParent<EnemySpawning>();
                anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("player");
        pM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        transform.parent = null;

        rb = GetComponent<Rigidbody2D>();

        sprRen = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteNo = Random.Range(0, ships.Length);
        Debug.Log(spriteNo);
        Debug.Log(ships[spriteNo]);
        sprRen.sprite = ships[spriteNo];
    }

    void FixedUpdate()
    {
        sprRen.sprite = ships[spriteNo];

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime * (Time.time/100+1));

        var dir = player.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            anim.runtimeAnimatorController = rtac;
            anim.Play("Explosion");
            enemySpawning.Kill(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        switch (collision.tag) 
        {
            case "Bullet":
                Destroy(collision.gameObject);
                TakeDamage(10);
                break;

            case "Enemy":
                break;

            case "player":
                Debug.Log("Collided with player");
                pM.TakeDamage(10);
                TakeDamage(10);
                break;

            case "Shield":
                //Bounce off??S
                break;
        }

    }
}
