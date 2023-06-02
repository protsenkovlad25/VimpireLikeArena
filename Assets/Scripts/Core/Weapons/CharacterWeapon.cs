using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public class CharacterWeapon : INeeding<IAttaching>
    {
        private List<WeaponBehaviour> m_Weapons;
        private IAttaching m_Attaching;

        public CharacterWeapon()
        {
            m_Weapons = new List<WeaponBehaviour>();
        }

        public void Init()
        {
            foreach (var item in m_Weapons)
            {
                item.Init();
            }
        }

        public void Start()
        {
            foreach (var item in m_Weapons)
            {
                item.Shoot();
            }
        }

        public void Stop()
        {
            foreach (var item in m_Weapons)
            {
                item.Stop();
            }
        }

        public void AddWeapon(WeaponBehaviour weapon)
        {
            if (weapon == null)
            {
                Debug.LogError($"Class: {nameof(CharacterWeapon)}:" +
                    $"\nMethode: - {nameof(AddWeapon)}. Null References");
                return;
            }

            if (weapon.TryGetComponent<ProjectileWeapon>(out var projectileWeapon))
            {
                projectileWeapon.Set(m_Attaching);
            }

            if (weapon.TryGetComponent<DirectWeapon>(out var directWeapon))
            {
                directWeapon.Set(m_Attaching);
            }

            m_Weapons.Add(weapon);
        }

        public void RemoveWeapon(WeaponBehaviour weapon)
        {
            if (weapon == null)
            {
                Debug.LogError($"Class: {nameof(CharacterWeapon)}:" +
                    $"\nMethode: - {nameof(RemoveWeapon)}. Null References");
                return;
            }

            m_Weapons.Remove(weapon);
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;

            foreach (var item in m_Weapons)
            {
                if (item.TryGetComponent<INeeding<IAttaching>>(out var projectileWeapon))
                {
                    projectileWeapon.Set(m_Attaching);
                }
            }
        }
    }
}