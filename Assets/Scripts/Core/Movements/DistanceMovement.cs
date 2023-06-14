using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class DistanceMovement : IMoving
    {
        private bool m_IsStop;
        private int m_MinDistance = 5;
        private int m_CheckDistance = 8;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            if (m_IsStop)
            {
                return;
            }

            var direction = (target - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);

            Vector3 difference = target - transform.position;
            Vector3 retreatDirection = transform.position + -difference.normalized * m_CheckDistance;

            if (difference.magnitude < m_MinDistance)
            {
                if (Physics.Raycast(retreatDirection, new Vector3(0, -1, 0), 5))
                {
                    transform.position = Vector3.MoveTowards(transform.position, retreatDirection, speed);
                }
                else
                {
                    var randomAngle = Random.Range(-90f, 90f);
                    int repeat = 6;

                    while (repeat != 0)
                    {
                        var newDirection = Quaternion.Euler(0, randomAngle, 0) * -difference.normalized;
                        if (Physics.Raycast(transform.position + newDirection * m_CheckDistance, new Vector3(0, -1, 0), 5))
                        {
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + newDirection * m_CheckDistance, speed);
                            break;
                        }
                        randomAngle = Random.Range(-90f, 90f);
                        repeat--;
                    }
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
