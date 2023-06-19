using UnityEngine;

namespace VampireLike.Core.Cameras
{
    public class LimitedCamera : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private Transform m_Target;

        private Vector3 m_RightPos;
        private Vector3 m_LeftPos;
        private Vector3 m_Position;

        public void Init()
        {
            m_RightPos = new Vector3(m_Camera.pixelWidth, m_Camera.pixelHeight / 2, 0);
            m_LeftPos = new Vector3(0, m_Camera.pixelHeight / 2, 0);

            m_Position = m_Target.position - m_Camera.transform.position;
        }

        private void Update()
        {
            Ray leftRay = m_Camera.ScreenPointToRay(m_LeftPos);
            Ray rightRay = m_Camera.ScreenPointToRay(m_RightPos);

            float deltaPosX = m_Target.position.x - m_Position.x;

            if ((deltaPosX > 0 && Physics.Raycast(rightRay, 50)) || (deltaPosX < 0 && Physics.Raycast(leftRay, 50)))
            {
                m_Camera.transform.position += new Vector3(deltaPosX, 0, 0);

            }


            m_Position = m_Target.position;
        }
    }
}
