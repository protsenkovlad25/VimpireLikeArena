using UnityEngine;

namespace VampireLike.Core.Looks
{
    public class SimpleLook : ILooking
    {
        private Vector3 m_BypassPosition;

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
                return Bypass(directionToHero, transform, hitPoint);
            }
            else
            {
                m_BypassPosition = Vector3.zero;
                return target;
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

                return m_BypassPosition = transform.position + (newDirection.normalized *distance);
            }
            else
                return m_BypassPosition;
        }

        public void ClearLook()
        {
            m_BypassPosition = Vector3.zero;
        }

        //public Vector3 Look(Vector3 target, Transform transform)
        //{
        //    Vector3 directionToHero;
        //    RaycastHit[] hits;

        //    if (m_IsBypass)
        //    {
        //        if ((m_BypassPosition - transform.position).magnitude > 1f)
        //        {
        //            directionToHero = (target - transform.position).normalized;
        //            hits = Physics.RaycastAll(transform.position, directionToHero, 10f);

        //            foreach (var hit in hits)
        //            {
        //                if (hit.collider.gameObject.CompareTag("Wall"))
        //                {
        //                    return m_BypassPosition;
        //                }
        //            }
        //        }

        //        m_IsBypass = false;
        //        return target;
        //    }
        //    else
        //    {
        //        directionToHero = (target - transform.position).normalized;
        //        hits = Physics.RaycastAll(transform.position, directionToHero, 10f);

        //        foreach (var hit in hits)
        //        {
        //            if (hit.collider.gameObject.CompareTag("Wall"))
        //            {
        //                return SearchBypass(transform, directionToHero);
        //            }
        //        }
        //    }

        //    return target;
        //}

        //public Vector3 SearchBypass(Transform transform, Vector3 directionToHero)
        //{
        //    int i = 1;
        //    float angle = 0;
        //    bool leftDir = true;
        //    bool rightDir = true;

        //    Vector3 newDirection;
        //    RaycastHit[] hits;

        //    do
        //    {
        //        if (angle >= 60f)
        //        {
        //            angle = Random.value > .5f ? angle : -angle;
        //            newDirection = Quaternion.Euler(0, angle, 0) * directionToHero;
        //            break;
        //        }
        //        angle = 5f * i;

        //        newDirection = Quaternion.Euler(0, angle, 0) * directionToHero;
        //        hits = Physics.RaycastAll(transform.position, newDirection, m_CheckDistance);

        //        foreach (var hit in hits)
        //        {
        //            if (hit.collider.gameObject.CompareTag("Wall"))
        //                rightDir = false;
        //        }

        //        if (!rightDir)
        //        {
        //            newDirection = Quaternion.Euler(0, -angle, 0) * directionToHero;
        //            hits = Physics.RaycastAll(transform.position, newDirection, m_CheckDistance);

        //            foreach (var hit in hits)
        //            {
        //                if (hit.collider.gameObject.CompareTag("Wall"))
        //                    leftDir = false;
        //            }

        //            if (leftDir)
        //                break;
        //        }
        //        else
        //            break;

        //        i++;
        //    }
        //    while (leftDir != true || rightDir != true);

        //    m_IsBypass = true;
        //    return m_BypassPosition = transform.position + newDirection * m_CheckDistance;
        //}

        public bool LookShooting()
        {
            return true;
        }
    }
}
