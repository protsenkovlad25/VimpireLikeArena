using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core
{
    public interface ILooking
    {
        Vector3 Look(Vector3 direction, Transform transform, float speed);

        public void ClearLook();

        public bool LookShooting();
    }
}
