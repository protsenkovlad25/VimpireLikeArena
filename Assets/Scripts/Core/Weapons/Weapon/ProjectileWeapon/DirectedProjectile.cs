using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public class DirectedProjectile : Projectile
    {
        private Vector3 m_Target;


        public override void Move(float speed, Vector3 point, float distance)
        {
            m_Target = point;

            base.Move(speed, point, distance);
        }

        private void Update()
        {
            if (!m_IsMove)
            {
                gameObject.SetActive(false);
                return;
            }

            var step = m_Speed * Time.deltaTime;
            var oldPostion = transform.position;

            m_Moving.Move(m_Target, step, transform);

            if (Vector3.Distance(m_StartPosition, transform.position) >= m_Distance)
            {
                m_IsMove = false;
                gameObject.SetActive(false);
            }

            if (Vector3.Distance(oldPostion, transform.position) <= float.Epsilon)
            {
                m_IsMove = false;
                gameObject.SetActive(false);
            }
        }
    }
}