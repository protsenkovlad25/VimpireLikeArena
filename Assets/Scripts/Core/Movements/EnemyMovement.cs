using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class EnemyMovement : IMoving
    {
        private bool m_IsStop;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            if (m_IsStop)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            var direction = (target - transform.position).normalized;

            var lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);
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