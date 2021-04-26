using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Scrollbar hpBar;

    public int maxHP = 100;
    float currentHP;

    void Awake()
    {
        currentHP = maxHP;
        TakeDamage(0);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

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
}
