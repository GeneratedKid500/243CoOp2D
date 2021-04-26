using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiringScript : MonoBehaviour
{
    public GameObject enemyBullets;
    bool firing;
    // Start is called before the first frame update
    void Start()
    {
        firing = true;
        StartCoroutine("startFiring");
    }

    IEnumerator startFiring()
    {
        while (firing)
        {
            Instantiate(enemyBullets, gameObject.transform.position, enemyBullets.transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
