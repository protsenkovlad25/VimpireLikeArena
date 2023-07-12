using UnityEngine;
using VampireLike.Core.Cameras;

namespace VampireLike.Core.General
{
    public class MISCController : MonoBehaviour
    {
        [SerializeField] private ShakeCamera m_ShakeCamera;
        [SerializeField] private LimitedCamera m_LimitedCamera;

        public void Init()
        {
            EventManager.OnStartArena.AddListener(ChangeCameraLimit);
            m_ShakeCamera.Init();
            m_LimitedCamera.Init();
        }

        public void ChangeCameraLimit()
        {
            m_LimitedCamera.ChangeLimit();
        }
    }
}