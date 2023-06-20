using UnityEngine;

namespace VampireLike.Core.Cameras
{
    public class LimitedCamera : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private Transform m_Target;

        private Vector3 m_RightPos;
        private Vector3 m_LeftPos;
        private Vector3 m_StartPosition;
        private Vector3 m_LastPosition;
        private bool m_IsLimited;

        public void Init()
        {
            m_RightPos = new Vector3(m_Camera.pixelWidth, m_Camera.pixelHeight / 2, 0);
            m_LeftPos = new Vector3(0, m_Camera.pixelHeight / 2, 0);

            m_StartPosition = m_Target.position - m_Camera.transform.position;
            m_LastPosition = m_Target.position - m_Camera.transform.position;

            m_IsLimited = false;
        }

        private void Update()
        {
            if (m_IsLimited)
                LimitMovement();
            else
            {
                Vector3 interpolatePosition = Vector3.Lerp(m_Camera.transform.position, new Vector3(m_Target.position.x - m_StartPosition.x, m_Camera.transform.position.y, m_Target.position.z - m_StartPosition.z), 0.1f);
                NotLimitMovement(interpolatePosition);
            }

            m_LastPosition = m_Target.position;
        }

        private void LimitMovement()
        {
            Ray leftRay = m_Camera.ScreenPointToRay(m_LeftPos);
            Ray rightRay = m_Camera.ScreenPointToRay(m_RightPos);

            float deltaPosX = m_Target.position.x - m_LastPosition.x;
            float deltaPosZ = m_Target.position.z - m_LastPosition.z; //m_LimitOnZ ? 0 : m_Target.position.z - m_StartPosition.z;

            if ((deltaPosX > 0 && Physics.Raycast(rightRay, out RaycastHit hitR, 50)))
            {
                if (hitR.collider.TryGetComponent<OnColiderEnterComponent>(out OnColiderEnterComponent c))
                    m_Camera.transform.position += new Vector3(deltaPosX, 0, deltaPosZ);

            }
            else if (deltaPosX < 0 && Physics.Raycast(leftRay, out RaycastHit hitL, 50))
            {
                if (hitL.collider.TryGetComponent<OnColiderEnterComponent>(out OnColiderEnterComponent c))
                    m_Camera.transform.position += new Vector3(deltaPosX, 0, deltaPosZ);
            }
        }

        private void NotLimitMovement(Vector3 newPosition)
        {
            m_Camera.transform.position = newPosition;
        }

        public void ChangeLimit()
        {
            m_IsLimited = m_IsLimited != true;
        }
    }
}
