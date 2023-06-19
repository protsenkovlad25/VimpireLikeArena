using UnityEngine;

namespace VampireLike.Core.Cameras
{
    public class FollowerCamera : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private Camera m_Camera;

        private Vector3 m_Position;
        private bool m_IsFix;

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }

        public void FixPosition()
        {
            m_IsFix = true;

            m_Position = m_Target.position - m_Camera.transform.position;
        }

        private void Update()
        {
            if (!m_IsFix)
            {
                return;
            }

            m_Camera.transform.position = new Vector3(m_Target.position.x - m_Position.x, m_Camera.transform.position.y, m_Target.position.z - m_Position.z);
        }
    }
}