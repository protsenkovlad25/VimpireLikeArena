using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core
{
    public interface IWeapon
    {
        void Activate();

        void Stop();

        void Shoot(Vector3 target);
    }
}