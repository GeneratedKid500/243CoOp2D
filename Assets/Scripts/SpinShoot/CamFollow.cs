using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float distance = -10;


    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, distance);
    }
}
