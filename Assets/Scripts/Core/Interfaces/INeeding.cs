using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core
{
    public interface INeeding<T>
    {
        void Set(T generic);
    }
}