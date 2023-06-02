using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public interface INeedingWeapon : INeeding<WeaponBehaviour>
    {
        WeaponType GetWeaponType();

        Transform Where();
    }
}