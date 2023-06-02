using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        protected WeaponData m_WeaponData;

        public abstract void Init();

        public abstract void Shoot();

        public abstract void Stop();

        public virtual void SetWeaponData(WeaponData weaponData)
        {
            if (weaponData == null)
            {
                Debug.LogError($"Class: {nameof(ProjectileWeapon)}." +
                    $"\nMethode: - {nameof(SetWeaponData)}. Null References - {nameof(weaponData)}");
                return;
            }

            m_WeaponData = weaponData;
        }
    }
}