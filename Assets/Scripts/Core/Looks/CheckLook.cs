using UnityEngine;

namespace VampireLike.Core.Looks
{
    public class CheckLook : ILooking
    {
        private float m_CheckDistanceStepBack = 7f;
        private float m_MinDistanceToHero = 6f;
        private Vector3 m_BypassPosition = Vector3.zero;
        private MovementState m_State = MovementState.Stand;

        private enum MovementState { Bypass, Stand, StepBack }

        public Vector3 Look(Vector3 target, Transform transform)
        {
            Vector3 directionToHero;
            Vector3 vecNormali;
            RaycastHit[] hits1;
            RaycastHit[] hits2;
            bool hitWall1 = false;
            bool hitWall2 = false;

            directionToHero = target - transform.position;

            vecNormali = Quaternion.Euler(0, 90, 0) * directionToHero.normalized;
            hits1 = Physics.RaycastAll(transform.position + vecNormali.normalized * -1f, directionToHero.normalized, directionToHero.magnitude);
            hits2 = Physics.RaycastAll(transform.position + vecNormali.normalized * 1f, directionToHero.normalized, directionToHero.magnitude);

            Vector3 hitPoint = Vector3.zero;

            foreach (var hit in hits1)
                if (hit.collider.TryGetComponent<SolidObject>(out _))
                {
                    hitWall1 = true;
                    hitPoint = hit.point;
                    break;
                }

            foreach (var hit in hits2)
                if (hit.collider.TryGetComponent<SolidObject>(out _))
                {
                    hitWall1 = true;
                    hitPoint = hit.point;
                    break;
                }

            if (hitWall1 || hitWall2)
            {
                m_State = MovementState.Bypass;
                return Bypass(directionToHero, transform, hitPoint);
            }

            if (directionToHero.magnitude > m_MinDistanceToHero)
            {
                m_State = MovementState.Stand;
                m_BypassPosition = Vector3.zero;
                return transform.position;
            }
            else
            {
                m_State = MovementState.StepBack;
                return StepBack(directionToHero, transform);
            }
        }

        public Vector3 Bypass(Vector3 directionToHero, Transform transform, Vector3 hitPoint)
        {
            if ((transform.position - m_BypassPosition).magnitude <= 1f || m_BypassPosition == Vector3.zero)
            {
                int i = 1;
                float angle = 0;
                float distance;
                bool leftDir1;
                bool leftDir2;
                bool rightDir1;
                bool rightDir2;

                Vector3 newDirection;
                Vector3 vecNormali;
                RaycastHit[] hits1;
                RaycastHit[] hits2;

                do
                {
                    leftDir1 = true;
                    leftDir2 = true;
                    rightDir1 = true;
                    rightDir2 = true;

                    if (angle >= 180f)
                    {
                        angle = Random.value > .5f ? angle : -angle;
                        newDirection = Quaternion.Euler(0, angle, 0) * directionToHero.normalized;
                        break;
                    }
                    angle = 5f * i;

                    newDirection = Quaternion.Euler(0, angle, 0) * directionToHero.normalized;
                    vecNormali = Quaternion.Euler(0, 90f, 0) * newDirection.normalized;
                    hits1 = Physics.RaycastAll(transform.position + vecNormali.normalized * -1f, newDirection, directionToHero.magnitude);
                    hits2 = Physics.RaycastAll(transform.position + vecNormali.normalized * 1f, newDirection, directionToHero.magnitude);

                    foreach (var hit in hits1)
                        if (hit.collider.TryGetComponent<SolidObject>(out _))
                        {
                            rightDir1 = false;
                            hitPoint = hit.point;
                            break;
                        }

                    foreach (var hit in hits2)
                        if (hit.collider.TryGetComponent<SolidObject>(out _))
                        {
                            rightDir2 = false;
                            hitPoint = hit.point;
                            break;
                        }

                    if (rightDir1 && rightDir2) break;
                    else
                    {
                        newDirection = Quaternion.Euler(0, -angle, 0) * directionToHero.normalized;
                        vecNormali = Quaternion.Euler(0, -90f, 0) * newDirection.normalized;
                        hits1 = Physics.RaycastAll(transform.position + vecNormali.normalized * -1f, newDirection, directionToHero.magnitude);
                        hits2 = Physics.RaycastAll(transform.position + vecNormali.normalized * 1f, newDirection, directionToHero.magnitude);

                        foreach (var hit in hits1)
                            if (hit.collider.TryGetComponent<SolidObject>(out _))
                            {
                                leftDir1 = false;
                                hitPoint = hit.point;
                                break;
                            }

                        foreach (var hit in hits2)
                            if (hit.collider.TryGetComponent<SolidObject>(out _))
                            {
                                leftDir2 = false;
                                hitPoint = hit.point;
                                break;
                            }

                        if (leftDir1 && leftDir2)
                            break;
                    }

                    i++;
                }
                while ((!leftDir1 && !leftDir2) || (!rightDir1 && !rightDir2));

                distance = (hitPoint - transform.position).magnitude;
                if (distance < 3) distance = 3;

                return m_BypassPosition = transform.position + (newDirection.normalized * distance);
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

        public void ClearLook()
        {
            m_BypassPosition = Vector3.zero;
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
