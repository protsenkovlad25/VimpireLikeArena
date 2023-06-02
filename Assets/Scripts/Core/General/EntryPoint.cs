using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.General
{

    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ControllersManager m_ControllersManager;

        private void Awake()
        {
            m_ControllersManager.ControllersInit();
        }
    }
}