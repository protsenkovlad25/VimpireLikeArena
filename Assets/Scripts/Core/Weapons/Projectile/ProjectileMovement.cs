using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public class ProjectileMovement : IMoving
    {
        public void Move(Vector3 target, float speed, Transform transform)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);

            var direction = (target - transform.position).normalized;

            var lookRotation = Quaternion.LookRotation(direction);

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