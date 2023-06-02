using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Cameras;

namespace VampireLike.Core
{
    public class MISCController : MonoBehaviour
    {
        [SerializeField] private FollowerCamera m_FollowerCamera;
        [SerializeField] private ShakeCamera m_ShakeCamera;

        public void Init()
        {
            m_FollowerCamera.FixPostion();
            m_ShakeCamera.Init();
        }
    }
}