using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class EnemyMovement : IMoving, IBypassing
    {
        private bool m_IsStop;
        private int m_CheckDistance = 10;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            if (m_IsStop)
            {
                return;
            }

            //var lookRotation = Quaternion.LookRotation((target - transform.position).normalized);
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);

            Vector3 difference = target - transform.position;
            Vector3 direction = transform.position + difference.normalized * m_CheckDistance;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, difference.normalized, out hit, 10f))
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    Bypass(speed, transform, difference);
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

        public void Bypass(float speed, Transform transform, Vector3 difference)
        {
            var randomAngle = Random.Range(-90f, 90f);
            int repeat = 6;

            while (repeat != 0)
            {
                var newDirection = Quaternion.Euler(0, randomAngle, 0) * difference.normalized;
                if (Physics.Raycast(transform.position + newDirection * m_CheckDistance, difference.normalized, 10f))
                {
                    var lookRotation = Quaternion.LookRotation(transform.position + newDirection * m_CheckDistance);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + newDirection * m_CheckDistance, speed);
                    break;
                }
                randomAngle = Random.Range(-90f, 90f);
                repeat--;
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