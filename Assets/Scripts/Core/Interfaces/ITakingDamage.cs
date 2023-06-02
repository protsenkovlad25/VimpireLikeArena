using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core
{
    public interface ITakingDamage
    {
        void TakeDamage(int damage);
    }
}