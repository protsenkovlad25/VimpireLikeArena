using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class EnemyMovement : IMoving, IBypassing
    {
        private bool m_IsStop;
        private bool m_IsBypass;
        private Vector3 m_NewPosition;
        private int m_CheckDistance = 10;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            if (m_IsStop)
            {
                return;
            }

            if (m_IsBypass)
            {
                if (transform.position != m_NewPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_NewPosition, speed);
                    return;
                }
                else
                {
                    m_IsBypass = false;
                }
            }

            Vector3 difference = target - transform.position;
            Vector3 direction = transform.position + difference.normalized * m_CheckDistance;

            if (Physics.Raycast(transform.position, difference.normalized, out RaycastHit hit, 3f))
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    transform.rotation = hit.collider.gameObject.transform.rotation;
                    Bypass(speed, transform, difference, hit.collider.gameObject.transform.localScale.x / 2);
                    Debug.Log("BYPASS");
                }
                else
                {
                    var lookRotation = Quaternion.LookRotation((target - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);
                    transform.position = Vector3.MoveTowards(transform.position, target, speed);
                }
            }
            else
            {
                var lookRotation = Quaternion.LookRotation((target - transform.position).normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);
                transform.position = Vector3.MoveTowards(transform.position, target, speed);
            }
        }

        public void Bypass(float speed, Transform transform, Vector3 difference, float halfLengthWall)
        {
            var randomAngle = Random.value < 0.5f ? 90f : -90f;
            var newDirection = Quaternion.Euler(0, randomAngle, 0) * difference.normalized;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + newDirection * halfLengthWall, speed);
            m_NewPosition = transform.position + newDirection * halfLengthWall;
            m_IsBypass = true;
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