using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mark : MonoBehaviour
{
    private void Awake()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        Sequence s = DOTween.Sequence();

        s.Append(sprite.DOColor(new Color(1, 1, 1, .2f),.25f));
        s.Append(sprite.DOColor(new Color(1, 1, 1, 1), .25f));
        s.Append(sprite.DOColor(new Color(1, 1, 1, .2f), .25f));
        s.Append(sprite.DOColor(new Color(1, 1, 1, 1), .25f));
        s.Append(sprite.DOColor(new Color(1, 1, 1, .2f), .25f));
        s.Append(sprite.DOColor(new Color(1, 1, 1, 1), .25f));

        s.AppendCallback(new TweenCallback(Destroy));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
