using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core
{
    public interface IBuilder<TResult>
    {
        TResult Build();
    }
}