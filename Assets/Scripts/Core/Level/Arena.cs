using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    public class Arena : MonoBehaviour
    {
        public event Action OnSteppedArena;

        [SerializeField] private Transform m_StartPoint;
        [SerializeField] private Transform m_EndPoint;
        [SerializeField] private Transform m_CenterArena;

        public Transform StartPoint => m_StartPoint;
        public Transform EndPoint => m_EndPoint;
        public Transform CenterArena => m_CenterArena;

        private Tween m_Tween;
        [SerializeField] [Tooltip("Не применять на префаб")] private bool m_IsEnter;//TODO "кастыль" для первой платформы

        public void Scale(Vector3 scale)
        {
            m_Tween?.Kill();

            m_Tween = DOTween.To(() => transform.localScale,
                                setter => transform.localScale = setter,
                                scale,
                                1f)
                        .SetEase(Ease.InOutBack);

            m_Tween.Play();
        }

        public void CollisionEnter(Collision collision)
        {
            if (m_IsEnter)
            {
                return;
            }
            Debug.LogError("Hehe boy");

            if (collision.gameObject.TryGetComponent<IHero>(out var hero))
            {
                OnSteppedArena?.Invoke();
                m_IsEnter = true;
            }
        }
    }
}