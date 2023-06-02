using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core
{
    public interface IRepelled
    {
        void Push(Vector3 direction, float force);
    }
}