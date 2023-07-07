using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public interface INeedingWeapon : INeeding<WeaponBehaviour>
    {
        List<WeaponType> GetWeaponTypes();

        List<Transform> GetWeaponPoints();

        public void SetWeaponType(WeaponType weaponType);

        Transform Where();
    }
}