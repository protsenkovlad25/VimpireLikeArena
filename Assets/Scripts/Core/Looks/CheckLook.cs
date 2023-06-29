using UnityEngine;

namespace VampireLike.Core.Looks
{
    public class CheckLook : ILooking
    {
        private float m_CheckDistanceStepBack = 8f;
        private float m_MinDistanceToHero = 5f;
        private Vector3 m_BypassPosition;
        private MovementState m_State;

        private enum MovementState { Bypass, Stand, StepBack }

        public Vector3 Look(Vector3 target, Transform transform)
        {
            Vector3 directionToHero = target - transform.position; ;
            RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToHero.normalized, directionToHero.magnitude); ;

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    m_State = MovementState.Bypass;
                    return Bypass(directionToHero, transform);
                }
            }

            if (directionToHero.magnitude > m_MinDistanceToHero)
            {
                m_State = MovementState.Stand;
                return transform.position;
            }
            else
            {
                m_State = MovementState.StepBack;
                return StepBack(target, transform);
            }
        }

        public Vector3 Bypass(Vector3 directionToHero, Transform transform)
        {
            if ((transform.position - m_BypassPosition).magnitude <= 1f)
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
                        newDirection = Quaternion.Euler(0, angle, 0) * directionToHero.normalized;
                        break;
                    }
                    angle = 5f * i;

                    newDirection = Quaternion.Euler(0, angle, 0) * directionToHero.normalized;
                    hits = Physics.RaycastAll(transform.position, newDirection, directionToHero.magnitude);

                    foreach (var hit in hits)
                    {
                        if (hit.collider.gameObject.CompareTag("Wall"))
                            rightDir = false;
                    }

                    if (!rightDir)
                    {
                        newDirection = Quaternion.Euler(0, -angle, 0) * directionToHero.normalized;
                        hits = Physics.RaycastAll(transform.position, newDirection, directionToHero.magnitude);

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

                return m_BypassPosition = transform.position + newDirection * directionToHero.magnitude;
            }
            else
                return m_BypassPosition;
        }

        public Vector3 StepBack(Vector3 directionToHero, Transform transform)
        {
            Vector3 retreatDirection = transform.position + -directionToHero.normalized * m_CheckDistanceStepBack;

            if (Physics.Raycast(retreatDirection, new Vector3(0, -1, 0), 5f))
            {
                return retreatDirection;
            }
            else
            {
                var randomAngle = Random.Range(-90f, 90f);
                int repeat = 6;

                while (repeat != 0)
                {
                    var newDirection = Quaternion.Euler(0, randomAngle, 0) * -directionToHero.normalized;
                    if (Physics.Raycast(transform.position + newDirection * m_CheckDistanceStepBack, new Vector3(0, -1, 0), 5f))
                    {
                        return transform.position + newDirection * m_CheckDistanceStepBack;
                    }
                    randomAngle = Random.Range(-90f, 90f);
                    repeat--;
                }

                return transform.position;
            }
        }

        public bool LookShooting()
        {
            return m_State switch
            {
                MovementState.Bypass => false,
                MovementState.Stand => true,
                MovementState.StepBack => true,
                _ => true,
            };
        }
    }
}
