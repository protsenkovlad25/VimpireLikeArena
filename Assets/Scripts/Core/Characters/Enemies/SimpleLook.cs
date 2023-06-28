using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core;

public class SimpleLook : ILooking
{
    public Vector3 Look(Vector3 target, Transform transform)
    {
        return target;
    }
}
