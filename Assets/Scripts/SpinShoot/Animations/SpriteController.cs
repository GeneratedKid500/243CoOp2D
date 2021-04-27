using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public Sprite[] _animation;

    int counter = 0;
    SpriteRenderer sprRend;

    private void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
    }

    public IEnumerator playAnimation()
    {
        while (sprRend.sprite != _animation[_animation.Length-1])
        {
            sprRend.sprite = _animation[counter];
            counter++;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        counter = 0;
    }
}
