using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class RocketProjectileMovement : IMoving
    {
        public Action OnRocketMoveEnd;
        public bool isMove;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            //rigidbody.MovePosition(transform.position + target * Time.deltaTime * speed);

            var direction = (target - transform.position).normalized;

            var lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);

            if (Mathf.Abs((transform.position - target).magnitude) < .01F)
            {
                OnRocketMoveEnd.Invoke();
            }
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
