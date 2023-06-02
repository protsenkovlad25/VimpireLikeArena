using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core
{
    public interface IMoving
    {
        void Move(Vector3 direction, float speed, Transform transform);

        void Stop();

        void Start();
    }
}