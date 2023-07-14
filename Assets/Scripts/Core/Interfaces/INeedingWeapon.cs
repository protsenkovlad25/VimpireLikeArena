using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public interface INeedingWeapon : INeeding<GameObject>
    {
        List<Transform> GetWeaponPoints();

        List<GameObject> GetWeaponPrefabs();

        Transform Where();
    }
}