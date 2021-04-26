using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShields : MonoBehaviour
{
    public Transform shieldPivot;

    GameObject[] p2Items;
    int counter = 0;

    private void Start()
    {
        Transform[] t = shieldPivot.GetComponentsInChildren<Transform>();
        p2Items = new GameObject[t.Length - 1];

        for (int i = 1; i < t.Length; i++)
        {
           p2Items[i-1] = t[i].gameObject;
        }

        foreach(GameObject item in p2Items)
        {
            item.SetActive(false);
        }
        p2Items[counter].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            p2Items[counter].SetActive(false);
            if (counter != p2Items.Length-1)
                counter++;
            else
                counter = 0;
            p2Items[counter].SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        shieldPivot.up = ShieldRotate();
    }

    Vector2 ShieldRotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePos.x - shieldPivot.position.x,
            mousePos.y - shieldPivot.position.y);

        return -direction;
    }

}
