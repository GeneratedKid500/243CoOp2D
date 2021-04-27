using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public GameObject bullet;
    public AudioClip laserPew;

    AudioSource audioSource;

    public float bulletSpeed;

    [Header("Technicals")]
    public Rigidbody2D mainBody;


    Transform parent;
    Camera cam;

    Vector2 dirToClick;

    bool clicked = false;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();

        parent = this.GetComponentInParent<Transform>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            dirToClick = cam.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
            dirToClick.Normalize();
        }
    }

    void FixedUpdate()
    {
        if (clicked)
        {
            audioSource.pitch = Random.Range(1f, 1.4f);
            audioSource.volume = 0.05f;
            audioSource.PlayOneShot(laserPew);

            GameObject b = Instantiate<GameObject>(bullet, transform.position, transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = (((bulletSpeed*100) * Time.fixedDeltaTime)* dirToClick);
            clicked = false;
            Destroy(b, 0.8f);
        }
    }
}
