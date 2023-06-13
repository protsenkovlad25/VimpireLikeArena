using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class DistanceMovement : IMoving
    {
        private bool m_IsStop;
        private int m_MinDistance = 5;
        private int m_CheckDistance = 2;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            if (m_IsStop)
            {
                return;
            }

            var direction = (target - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);

            var difference = target - transform.position;
            if (difference.magnitude < m_MinDistance)
            {
                if (Physics.Raycast(transform.position + -difference.normalized * m_CheckDistance, new Vector3(0, -1, 0), 5))
                {
                    Debug.Log((transform.position + -difference.normalized * m_CheckDistance).ToString());
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + -difference.normalized * m_CheckDistance, speed);
                }
            }
        }

        public void Start()
        {
            m_IsStop = false;
        }

        public void Stop()
        {
            m_IsStop = true;
        }
    }
}
