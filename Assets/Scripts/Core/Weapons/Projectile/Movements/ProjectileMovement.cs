using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class ProjectileMovement : IMoving
    {
        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            transform.position += speed * Time.deltaTime * target;

            var lookRotation = Quaternion.LookRotation(target);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}