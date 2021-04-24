using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splitscreen : MonoBehaviour
{
    public Image uiSplit;
    public Camera[] cameras;

    public GameObject[] players;

    Vector2 distBetween;
    bool xBorder1, yBorder1;
    bool xBorder2, yBorder2;

    void Start()
    {

    }

    void Update()
    {
        Vector3 play1pos = players[0].transform.position;
        Vector3 play2pos = players[1].transform.position;

        distBetween = new Vector2(play1pos.x - play2pos.x, play1pos.y - play2pos.y);
    }

    void LateUpdate()
    {
        cameras[0].transform.position = new Vector3(players[0].transform.position.x - distBetween.x/2, players[0].transform.position.y - distBetween.y/2, -10);

        isOnScreen();

        if (xBorder1 && yBorder1 && xBorder2 && yBorder2)
            CameraSwitch(true);
        else
            CameraSwitch(false);

    }

    void isOnScreen()
    {
        foreach(GameObject player in players)
        {
            if (cameras[0].WorldToViewportPoint(player.transform.position).y < 0 || cameras[0].WorldToViewportPoint(player.transform.position).y > 1)
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

            if (cameras[0].WorldToViewportPoint(player.transform.position).x < 0 || cameras[0].WorldToViewportPoint(player.transform.position).x > 1)
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
