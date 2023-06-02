using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VampireLike.Core
{
    public class OnColiderEnterComponent : MonoBehaviour
    {
        public UnityEvent<Collision> OnColiderEnter;

        private void OnCollisionEnter(Collision collision)
        {
            OnColiderEnter?.Invoke(collision);
        }
    }
}