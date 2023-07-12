using UnityEngine;

namespace VampireLike.Core.Characters
{
    public class DirectedMovement : IMoving
    {
        private bool m_IsStop;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            if (m_IsStop)
            {
                return;
            }

            var lookRotation = Quaternion.LookRotation((target - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);

            transform.position = Vector3.MoveTowards(transform.position, target, speed);
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
