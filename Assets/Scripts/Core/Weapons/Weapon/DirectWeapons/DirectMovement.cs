using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VampireLike.Core.Weapons
{
    public class DirectMovement : IMoving
    {
        public void Move(Vector3 target, float speed, Transform transform)
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