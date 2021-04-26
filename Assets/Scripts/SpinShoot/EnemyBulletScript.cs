using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject player;
    public Vector2 startPosition;
    public Vector2 midPositionA;
    public Vector2 midPositionB;
    public Vector2 playerPosition;
    public Vector2[] positions;
    public float curvePoints;
    // Start is called before the first frame update
    void Start()
    {
        curvePoints = 70f;
        positions = new Vector2[70];
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        playerPosition = player.transform.position;
        int direction = Random.Range(1, 3);
        if (direction == 1)
        {
            midPositionA = Vector2.Lerp(playerPosition, startPosition, 0.25f) + new Vector2(50, 0);
            midPositionB = Vector2.Lerp(playerPosition, startPosition, 0.75f) + new Vector2(-50, 0);
        }
        else if (direction == 2)
        {
            midPositionA = Vector2.Lerp(playerPosition, startPosition, 0.25f) + new Vector2(-50, 0);
            midPositionB = Vector2.Lerp(playerPosition, startPosition, 0.75f) + new Vector2(50, 0);
        }


        StartCoroutine("MoveLocation");
    }

    Vector2 GetCubicPoints(Vector2 point0, Vector2 point1, Vector2 point2, Vector2 point3, float t)
    {
        Vector2 point;
        point = Vector2.Lerp(Vector2.Lerp(Vector2.Lerp(point0, point1, t), Vector2.Lerp(point1, point2, t), t),
            Vector2.Lerp(Vector2.Lerp(point1, point2, t), Vector2.Lerp(point2, point3, t), t), t);
        return point;
    }
    IEnumerator MoveLocation()
    {
        for (int i = 1; i <= curvePoints; i++)
        {
            Vector2 newPosition;
            float t = i / curvePoints;
            newPosition = GetCubicPoints(startPosition, midPositionA, midPositionB, playerPosition, t);
            transform.position = newPosition;
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessageUpwards("ApplyHealthChange", -10, SendMessageOptions.DontRequireReceiver);
        }
    }
}
