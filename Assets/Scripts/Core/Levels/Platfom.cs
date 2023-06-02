using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    public class Platfom : MonoBehaviour
    {
        private bool m_IsExite;

        public event Action OnExitePlatform;
        public bool IsExite => m_IsExite;

        private Tween m_Tween;

        public void Move(Vector3 position)
        {
            m_Tween?.Kill();

            m_Tween = DOTween.To(() => transform.position,
                                    setter => transform.position = setter,
                                    transform.position + position,
                                    1.5f)
                            .SetEase(Ease.OutBack);

            m_Tween.Play();
        }

        public void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IHero>(out var hero))
            {
                m_IsExite = false;
                Move(Vector3.up * 10 * -1);
                OnExitePlatform?.Invoke();
            }
        }
    }
}