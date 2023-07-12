using UnityEngine;

namespace VampireLike.Core.Characters
{
    public class TestMovement : IMoving, IBypassing
    {
        private float m_CheckDistance = 10f;
        private bool m_IsStop;
        private bool m_IsBypass;
        private bool m_IsSearchBypass;
        private Vector3 m_BypassPosition;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            Vector3 directionToHero;
            RaycastHit[] hits;

            if (m_IsStop)
                return;

            if (m_IsBypass)
            {
                //if (transform.position == m_BypassPosition)
                //{
                //    m_IsBypass = false;
                //    return;
                //}

                //var lookRotation = Quaternion.LookRotation(m_BypassPosition.normalized);
                //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);

                //transform.position = Vector3.MoveTowards(transform.position, m_BypassPosition, speed);

                //directionToHero = (target - transform.position).normalized;
                //hits = Physics.RaycastAll(transform.position, directionToHero, m_CheckDistance);

                //foreach (var hit in hits)
                //{
                //    if (hit.collider.gameObject.tag == "Wall")
                //        return;
                //}

                //m_IsBypass = false;

                Debug.Log(m_BypassPosition.ToString());
            }
            else
            {
                if (!m_IsSearchBypass)
                {
                    directionToHero = (target - transform.position).normalized;
                    hits = Physics.RaycastAll(transform.position, directionToHero, m_CheckDistance);

                    foreach (var hit in hits)
                    {
                        if (hit.collider.gameObject.tag == "Wall")
                        {
                            Debug.Log("CHECK WALL");
                            m_IsSearchBypass = true;
                            SearchBypass(transform, directionToHero);
                            break;
                        }
                    }

                    var lookRotation = Quaternion.LookRotation((target - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);

                    transform.position = Vector3.MoveTowards(transform.position, target, speed);
                }
            }
        }

        public void SearchBypass(Transform transform, Vector3 directionToHero)
        {
            int i = 1;
            float angle;
            bool leftDir = true;
            bool rightDir = true;

            Vector3 newDirection;
            RaycastHit[] hits;

            do
            {
                angle = 5f * i;

                newDirection = Quaternion.Euler(0, angle, 0) * directionToHero;
                hits = Physics.RaycastAll(transform.position, newDirection, m_CheckDistance);

                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.tag == "Wall")
                        rightDir = false;
                }

                if (!rightDir)
                {
                    newDirection = Quaternion.Euler(0, -angle, 0) * directionToHero;
                    hits = Physics.RaycastAll(transform.position, newDirection, m_CheckDistance);

                    foreach (var hit in hits)
                    {
                        if (hit.collider.gameObject.tag == "Wall")
                            leftDir = false;
                    }

                    if (leftDir)
                    {
                        Debug.Log("LEFT BYPASS");
                        break;
                    }
                }
                else
                {
                    Debug.Log("RIGHT BYPASS");
                    break;
                }

                i++;
            }
            while (leftDir != true || rightDir != true);

            Debug.Log("BYPASS");
            m_IsSearchBypass = false;
            m_IsBypass = true;
            m_BypassPosition = transform.position + newDirection * m_CheckDistance;
        }

        public void Start()
        {
            m_IsStop = false;
        }

        public void Stop()
        {
            m_IsStop = true;
        }

        public void Bypass(float speed, Transform transform, Vector3 difference, float halfLengthWall)
        {

        }
    }
}
