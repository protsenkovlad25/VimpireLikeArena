using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Movements;
using DG.Tweening;

namespace VampireLike.Core.Weapons
{
    public class RocketShootingWeapon : WeaponBehaviour, INeeding<IAttaching>
    {
        [SerializeField] private List<RocketProjectile> m_RocketProjectiles;

        private IAttaching m_Attaching;
        private IMoving m_Moving;
        private ProjectileWeaponData m_ProjectileWeaponData;

        public override void Init()
        {
            m_Moving = new RocketProjectileMovement();
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public override void SetWeaponData(WeaponData weaponData)
        {
            if (weaponData == null)
            {
                Debug.LogError($"Class: {nameof(ProjectileWeapon)}." +
                    $"\nMethode: - {nameof(SetWeaponData)}. Null References - {nameof(weaponData)}");
                return;
            }

            var projectileWeaponData = weaponData as ProjectileWeaponData;

            if (projectileWeaponData == null)
            {
                Debug.LogError($"Class: {nameof(ProjectileWeapon)}." +
                    $"\nMethode: - {nameof(SetWeaponData)}. Failed to convert - {nameof(projectileWeaponData)}");
                return;
            }

            m_ProjectileWeaponData = projectileWeaponData;

            base.SetWeaponData(weaponData);
        }

        public override void Shoot()
        {
            StartCoroutine(ShootCoroutine());
        }

        public override void Stop()
        {
        }

        private IEnumerator ShootCoroutine()
        {
            int dir = -1;
            foreach(var rocket in m_RocketProjectiles)
            {
                rocket.Damage = m_ProjectileWeaponData.Damage;
                rocket.RepulsiveForce = m_ProjectileWeaponData.RepulsiveForce;

                //yield return new WaitForSeconds(5f);
                yield return new WaitForSeconds(m_ProjectileWeaponData.AttackSpeed);

                rocket.transform.DOMove(transform.position + rocket.transform.right * dir*3, .2f);
                dir = -dir;
                if (m_Attaching.GetTarget() == null)
                {
                    yield break;
                }
                rocket.SetMovement(m_Moving);
                rocket.Init();
                rocket.Move(m_ProjectileWeaponData.ProjectileSpeed, m_Attaching.GetTarget().position, m_ProjectileWeaponData.Distance);
            }
        }
    }
}
