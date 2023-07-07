using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public abstract class Projectile : MonoBehaviour
    {
        public event Action<Projectile> OnHit;

        public int Damage { get; set; }
        public float RepulsiveForce { get; set; }
        
        protected bool m_IsMove;
        protected float m_Speed;
        protected float m_FlyTime;

        protected Vector3 m_StartPosition;

        protected IMoving m_Moving;

        protected Vector3 m_Direction;


        public void SetMovement(IMoving moving)
        {
            m_Moving = moving;
        }

        public virtual void Move(float speed, Vector3 point, float flyTime)
        {
            m_Speed = speed;
            m_IsMove = true;
            m_FlyTime = flyTime;
            m_StartPosition = transform.position;
            m_Direction = (point - transform.position).normalized;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            m_IsMove = false;

            gameObject.SetActive(false);
            if (collision.gameObject.TryGetComponent<ITakingDamage>(out var takingDamage))
            {
                takingDamage.TakeDamage(Damage);
            }

            if (collision.gameObject.TryGetComponent<IRepelled>(out var repelled))
            {
                repelled.Push(transform.forward, RepulsiveForce);
            }

            Hit();
        }

        private void Hit()
        {
            gameObject.SetActive(false);

            OnHit?.Invoke(this);
        }

        protected virtual void Update()
        {

            if (!m_IsMove)
            {
                gameObject.SetActive(false);
                return;
            }

            m_FlyTime -= Time.deltaTime;
            
            var oldPostion = transform.position;

            m_Moving.Move(m_Direction, m_Speed, transform, gameObject.GetComponent<Rigidbody>());


            if (m_FlyTime <= 0)
            {
                m_IsMove = false;
                gameObject.SetActive(false);
            }

        }
    }
}