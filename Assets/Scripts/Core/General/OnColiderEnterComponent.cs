using UnityEngine;
using UnityEngine.Events;

namespace VampireLike.Core.General
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