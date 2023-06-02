using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action<Vector2> OnInput;

        [SerializeField] private Joystick m_Joystick;

        private void Update()
        {
            if (!m_Joystick.Direction.Equals(Vector2.zero))
            {
                OnInput?.Invoke(m_Joystick.Direction);
            }
        }
    }
}