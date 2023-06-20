using UnityEngine;
using DG.Tweening;

namespace VampireLike.Core.Cameras
{
    public class LimitedCamera : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private Transform m_Target;
        [SerializeField] private Transform m_Arena;
        [SerializeField] private int m_PercantageArenaVisibility;

        private Vector3 m_RightScreenPoint;
        private Vector3 m_LeftScreenPoint;
        private Vector3 m_StartPosition;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentPosition;
        private bool m_IsLimited;

        public void Init()
        {
            m_RightScreenPoint = new Vector3(m_Camera.pixelWidth, m_Camera.pixelHeight / 2, 0);
            m_LeftScreenPoint = new Vector3(0, m_Camera.pixelHeight / 2, 0);

            SettingCameraPosition();

            m_StartPosition = m_Target.position - m_Camera.transform.position;
            m_LastTargetPosition = m_Target.position - m_Camera.transform.position;

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

            m_LastTargetPosition = m_Target.position;
        }

        private void LimitMovement()
        {
            Ray leftRay = m_Camera.ScreenPointToRay(m_LeftScreenPoint);
            Ray rightRay = m_Camera.ScreenPointToRay(m_RightScreenPoint);

            float deltaPosX = m_Target.position.x - m_LastTargetPosition.x;
            float deltaPosZ = m_Target.position.z - m_LastTargetPosition.z; //m_LimitOnZ ? 0 : m_Target.position.z - m_StartPosition.z;

            if ((deltaPosX > 0 && Physics.Raycast(rightRay, out RaycastHit hitR, 100)))
            {
                if (hitR.collider.TryGetComponent<OnColiderEnterComponent>(out OnColiderEnterComponent c))
                    if (m_Camera.transform.position.x - m_Target.position.x < 1)
                    {
                        m_CurrentPosition += new Vector3(deltaPosX, 0, deltaPosZ);
                    }

            }
            else if (deltaPosX < 0 && Physics.Raycast(leftRay, out RaycastHit hitL, 100))
            {
                if (hitL.collider.TryGetComponent<OnColiderEnterComponent>(out OnColiderEnterComponent c))
                    if (m_Camera.transform.position.x - m_Target.position.x > -1)
                    {
                        m_CurrentPosition += new Vector3(deltaPosX, 0, deltaPosZ);
                    }
            }


        }

        private void FixedUpdate()
        {
            if(m_IsLimited)
            {
                m_Camera.transform.position = m_CurrentPosition;
            }
        }

        private void SettingCameraPosition()
        {
            float diameterArena = m_Arena.transform.localScale.x * 2;
            float requiredDistance = diameterArena * m_PercantageArenaVisibility / 100;

            m_CurrentPosition = m_Arena.transform.position-m_Camera.transform.forward * requiredDistance * Mathf.Sin(60 * Mathf.Deg2Rad);

            m_Camera.transform.DOMove(m_CurrentPosition,.5F);
        }

        private void NotLimitMovement(Vector3 newPosition)
        {
            m_Camera.transform.position = newPosition;
        }

        public void ChangeLimit()
        {
            m_IsLimited = !m_IsLimited;

            if(m_IsLimited)
            {
                RaycastHit[] hits = Physics.RaycastAll(m_Camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2)),100);

                foreach(var hit in hits)
                {
                    if(hit.collider.TryGetComponent<OnColiderEnterComponent>(out OnColiderEnterComponent c))
                    {
                        m_Arena = hit.transform;
                        break;
                    }
                }
                SettingCameraPosition();
            }
        }
    }
}
