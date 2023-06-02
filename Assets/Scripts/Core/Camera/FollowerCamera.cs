using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Cameras
{
    public class FollowerCamera : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private Camera m_Camera;

        private Vector3 m_Postion;
        private bool m_IsFix;

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }

        public void FixPostion()
        {
            m_IsFix = true;

            m_Postion = m_Target.position - m_Camera.transform.position;
        }

        private void Update()
        {
            if (!m_IsFix)
            {
                return;
            }

            m_Camera.transform.position = new Vector3(m_Target.position.x - m_Postion.x, m_Camera.transform.position.y, m_Target.position.z - m_Postion.z);
        }
    }
}