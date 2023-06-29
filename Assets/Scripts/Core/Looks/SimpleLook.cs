using UnityEngine;

namespace VampireLike.Core.Looks
{
    public class SimpleLook : ILooking
    {
        private float m_CheckDistance = 10f;
        private bool m_IsBypass;
        private Vector3 m_BypassPosition;

        public Vector3 Look(Vector3 target, Transform transform)
        {
            Vector3 directionToHero;
            RaycastHit[] hits;

            if (m_IsBypass)
            {
                if ((m_BypassPosition - transform.position).magnitude > 1f)
                {
                    directionToHero = (target - transform.position).normalized;
                    hits = Physics.RaycastAll(transform.position, directionToHero, 10f);

                    foreach (var hit in hits)
                    {
                        if (hit.collider.gameObject.CompareTag("Wall"))
                        {
                            return m_BypassPosition;
                        }
                    }
                }

                m_IsBypass = false;
                return target;
            }
            else
            {
                directionToHero = (target - transform.position).normalized;
                hits = Physics.RaycastAll(transform.position, directionToHero, 10f);

                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Wall"))
                    {
                        return SearchBypass(transform, directionToHero);
                    }
                }
            }

            return target;
        }

        public Vector3 SearchBypass(Transform transform, Vector3 directionToHero)
        {
            int i = 1;
            float angle = 0;
            bool leftDir = true;
            bool rightDir = true;

            Vector3 newDirection;
            RaycastHit[] hits;

            do
            {
                if (angle >= 60f)
                {
                    angle = Random.value > .5f ? angle : -angle;
                    newDirection = Quaternion.Euler(0, angle, 0) * directionToHero;
                    break;
                }
                angle = 5f * i;

                newDirection = Quaternion.Euler(0, angle, 0) * directionToHero;
                hits = Physics.RaycastAll(transform.position, newDirection, m_CheckDistance);

                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Wall"))
                        rightDir = false;
                }

                if (!rightDir)
                {
                    newDirection = Quaternion.Euler(0, -angle, 0) * directionToHero;
                    hits = Physics.RaycastAll(transform.position, newDirection, m_CheckDistance);

                    foreach (var hit in hits)
                    {
                        if (hit.collider.gameObject.CompareTag("Wall"))
                            leftDir = false;
                    }

                    if (leftDir)
                        break;
                }
                else
                    break;

                i++;
            }
            while (leftDir != true || rightDir != true);

            m_IsBypass = true;
            return m_BypassPosition = transform.position + newDirection * m_CheckDistance;
        }

        public bool LookShooting()
        {
            return true;
        }
    }
}
