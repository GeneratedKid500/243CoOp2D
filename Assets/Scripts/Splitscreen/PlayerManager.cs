using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Scrollbar hpBar;

    AudioSource audioSource;

    public int maxHP = 100;
    float currentHP;

    SpriteRenderer sprRend;
    public Sprite damageSprite;
    Sprite regularSprite;


    void Awake()
    {
        currentHP = maxHP;
        sprRend = GetComponentInChildren<SpriteRenderer>();

        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        regularSprite = sprRend.sprite;
        TakeDamage(0);
    }

    private void LateUpdate()
    {
        hpBar.value = 0;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        audioSource.pitch = 1;
        audioSource.volume = 0.2f;
        audioSource.Play();
        StartCoroutine(DamageTimer());

        if (maxHP % 10 == 0)
            hpBar.size = currentHP / 100;

        if (currentHP < 0)
        {
            Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #endif
        }


    }

    IEnumerator DamageTimer()
    {
        sprRend.sprite = damageSprite;

        yield return new WaitForSeconds(0.1f);

        sprRend.sprite = regularSprite;
    }
}
