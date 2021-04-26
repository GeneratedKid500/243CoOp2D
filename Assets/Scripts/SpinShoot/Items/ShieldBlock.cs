using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shield Collision with " + collision.tag);
        //Destroy(collision.gameObject);

        switch(collision.tag)
        {
            case "Bullet":
            case "EnemyBullet":
                Destroy(collision.gameObject);
                break;

            case "Enemy":
                //bounce off??
                break;

            default:
                break;
        }
    }
}
