using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splitscreen : MonoBehaviour
{
    public Image uiSplit;
    public Camera[] cameras;

    public GameObject[] players;

    public GameObject[] camBounds;

    Plane[] planes;
    float distBetween;
    bool xBorder1, yBorder1;
    bool xBorder2, yBorder2;

    void Start()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cameras[0]);
        for (int i = 0; i < 4; ++i)
        {
            camBounds[i].transform.position = -planes[i].normal * planes[i].distance;
            camBounds[i].transform.rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
        }
    }

    void Update()
    {
        distBetween = Vector2.Distance(players[0].transform.position, players[1].transform.position);

        for (int i = 0; i < 4; ++i)
        {
            Debug.Log(camBounds[i].transform.localPosition + " " + i);
        }
    }

    void LateUpdate()
    {
        IsOnScreen();

        //cameras[0].transform.position = new Vector3(players[0].transform.position.x, players[0].transform.position.y, -10);

        if (xBorder1 && yBorder1 && xBorder2 && yBorder2)
        {
            CameraSwitch(true);
        }
        else
        {
            CameraSwitch(false);
        }

    }

    void IsOnScreen()
    {
        foreach (GameObject plane in camBounds)
        {
            foreach (GameObject player in players)
            {
                if (Mathf.Abs(plane.transform.position.x) > Mathf.Abs(plane.transform.position.y))
                {
                    if (Mathf.Abs(player.transform.position.x) > Mathf.Abs(plane.transform.localPosition.x))
                    {
                        //Debug.Log("Player Outside of X border");
                        if (player == players[0])
                            xBorder1 = false;
                        else
                            xBorder2 = false;
                    }
                    else
                    {
                        //Debug.Log("Player inside of X border");
                        if (player == players[0])
                            xBorder1 = true;
                        else
                            xBorder2 = true;
                    }
                }
                else
                {
                    if (Mathf.Abs(player.transform.position.y) > Mathf.Abs(plane.transform.localPosition.y))
                    {
                        //Debug.Log("Player Outside of Y border");
                        if (player == players[0])
                            yBorder1 = false;
                        else
                            yBorder2 = false;
                    }
                    else
                    {
                        //Debug.Log("Player inside of Y border");
                        if (player == players[0])
                            yBorder1 = true;
                        else
                            yBorder2 = true;
                    }
                }
            }
        }
    }

    void CameraSwitch(bool switcher)
    {
        cameras[0].enabled = switcher;

        for (int i = 1; i <= 2; i++)
        {
            cameras[i].enabled = !switcher;
        }
        uiSplit.gameObject.SetActive(!switcher);
    }
}
