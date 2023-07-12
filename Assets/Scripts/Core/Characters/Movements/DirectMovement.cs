using UnityEngine;

namespace VampireLike.Core.Characters
{
    public class DirectMovement : IMoving
    {
        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            Debug.LogError(transform.eulerAngles);
        }

        public void Start()
        {
           
        }

        public void Stop()
        {
            
        }
    }
}