using System.Collections;
using UnityEngine;
using VampireLike.Core.Movements;

namespace VampireLike.Core.Weapons
{
    public class DirectWeapon : WeaponBehaviour, INeeding<IAttaching>
    {
        [SerializeField] private Transform m_StartPoint;
        [SerializeField] private bool m_IsStop;

        private IAttaching m_Attaching;
        private PoolBehaviour<Projectile> m_Pool;
        private ProjectileWeaponData m_ProjectileWeaponData;
        
        private IMoving m_Moving;

        public override void Init()
        {
            m_Pool = new PoolBehaviour<Projectile>();
            m_Pool.CreateParent(transform);
            m_Pool.Pooling(m_ProjectileWeaponData.ProjectilePref, 15);
            m_Moving = new DirectMovement();
        }

        public override void Shoot()
        {
            StartCoroutine(ShootCoroutine());
        }

        public override void Stop()
        {
            StopCoroutine(ShootCoroutine());
            m_IsStop = false;
            m_Pool.ReturnAllPull();
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

        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                if (m_IsStop)
                {
                    yield break;
                }

                var projectile = m_Pool.Take();
                projectile.transform.SetParent(null);
                projectile.gameObject.layer = gameObject.layer;
                projectile.transform.position = m_StartPoint.position;
                projectile.Damage = m_ProjectileWeaponData.Damage;
                projectile.RepulsiveForce = m_ProjectileWeaponData.RepulsiveForce;
                if (m_Attaching.GetTarget() == null)
                {
                    yield break;
                }
                projectile.SetMovement(m_Moving);
                projectile.Move(m_ProjectileWeaponData.ProjectileSpeed, m_Attaching.GetTarget().position, m_ProjectileWeaponData.Distance);

                yield return new WaitForSeconds(m_ProjectileWeaponData.AttackSpeed);
            }
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }
    }
}