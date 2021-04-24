using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    public bool playerA, playerB;
    public float speed = 10;

    Rigidbody2D rb;
    float vert, horz = 0;
    Vector2 moveDir;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime * 50;
        rb.velocity = new Vector2(moveDir.x * time * speed, moveDir.y * time * speed);
    }

    void Movement()
    {
        if (playerA && playerB)
        {
            Debug.LogError("A CharacterController Script attatched to " + gameObject.name + " is assigned both Player A and Player B");
            return;
        }

        else if (playerA && !playerB)
        {
            if (Input.GetKeyDown(KeyCode.W))
                vert++;
            else if (Input.GetKeyUp(KeyCode.W))
                vert--;

            if (Input.GetKeyDown(KeyCode.S))
                vert--;
            else if (Input.GetKeyUp(KeyCode.S))
                vert++;

            if (Input.GetKeyDown(KeyCode.A))
                horz--;
            else if (Input.GetKeyUp(KeyCode.A))
                horz++;

            if (Input.GetKeyDown(KeyCode.D))
                horz++;
            else if (Input.GetKeyUp(KeyCode.D))
                horz--;
        }

        else if (playerB && !playerA)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                vert++;
            else if (Input.GetKeyUp(KeyCode.UpArrow))
                vert--;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                vert--;
            else if (Input.GetKeyUp(KeyCode.DownArrow))
                vert++;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                horz--;
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
                horz++;

            if (Input.GetKeyDown(KeyCode.RightArrow))
                horz++;
            else if (Input.GetKeyUp(KeyCode.RightArrow))
                horz--;
        }

        else
        {
            Debug.LogError("A CharacterController Script attatched to " + gameObject.name + "  is not assigned as Player A or B");
            return;
        }

        moveDir = new Vector2(horz, vert);
        moveDir.Normalize();
    }
}
