using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public interface INeedingWeapon : INeeding<WeaponBehaviour>
    {
        List<WeaponVariant> GetWeaponVariants();

        List<Transform> GetWeaponPoints();

        public void SetWeaponVariant(WeaponVariant weaponVariant);

        Transform Where();
    }
}