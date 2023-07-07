using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using DG.Tweening;

public abstract class PickapblePrize : MonoBehaviour
{
    [SerializeField] protected Transform m_PrizePoint;

    public Action OnGet;

    public virtual void Initialize()
    {
        m_PrizePoint.transform.eulerAngles= new Vector3(60,0,0);

        Sequence s = DOTween.Sequence();
        s.Append(m_PrizePoint.DORotate(new Vector3(60, 120, 0), .5f).SetEase(Ease.Linear));
        s.Append(m_PrizePoint.DORotate(new Vector3(60, 240, 0), .5f).SetEase(Ease.Linear));
        s.Append(m_PrizePoint.DORotate(new Vector3(60, 0, 0), .5f).SetEase(Ease.Linear));
        s.SetLoops(-1);

        m_PrizePoint.localScale = new Vector3(2, 2, 2);
    }

    public abstract void GetPrize(MainCharacter mainCharacter);

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out MainCharacter mc))
        {
            GetPrize(mc);
            OnGet();
        }
    }
}
