using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters.Enemies;

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
            foreach (var weapon in m_Weapons)
            {
                weapon.Init();
            }
        }

        public void Shoot()
        {
            //if (m_Weapons != null)
                foreach (var weapon in m_Weapons)
                {
                    weapon.Shoot();
                }
        }

        public void StartShoot()
        {
            foreach (var weapon in m_Weapons)
                weapon.StartShoot();
        }

        public void Stop()
        {
            foreach (var weapon in m_Weapons)
            {
                weapon.StopShoot();
            }
        }

        public void AddWeapon(WeaponBehaviour weapon, EnemyCharacter enemyCharacter = null)
        {
            if (weapon == null)
            {
                Debug.LogError($"Class: {nameof(CharacterWeapon)}:" +
                    $"\nMethode: - {nameof(AddWeapon)}. Null References");
                return;
            }

            if (weapon.TryGetComponent<InfinityShootingWeapon>(out var infinityShootingWeapon))
            {
                infinityShootingWeapon.Set(m_Attaching);
            }

            if (weapon.TryGetComponent<SimpleShootingWeapon>(out var simpleShootingWeapon))
            {
                simpleShootingWeapon.Set(m_Attaching);
            }

            if(weapon.TryGetComponent<RocketShootingWeapon>(out var rocketShootingWeapon))
            {
                rocketShootingWeapon.Set(m_Attaching);
                
                if (enemyCharacter != null)
                    rocketShootingWeapon.SetEnemyCharacter(enemyCharacter);
            }

            if (weapon.TryGetComponent<ProjectileWeapon>(out var projectileWeapon))
            {
                projectileWeapon.Set(m_Attaching);
            }

            if (weapon.TryGetComponent<TestWeapon>(out var testWeapon))
            {
                testWeapon.Set(m_Attaching);
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