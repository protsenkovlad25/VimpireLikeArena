using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Movements;

namespace VampireLike.Core.Weapons
{
    public class SimpleShootingWeapon : WeaponBehaviour, INeeding<IAttaching>
    {
        [SerializeField] private Transform m_StartPoint;
        [SerializeField] private bool m_IsStop;
        [SerializeField] private ParticleSystem m_ShootParticle;

        private IAttaching m_Attaching;
        private PoolBehaviour<Projectile> m_Pool;
        private ProjectileWeaponData m_ProjectileWeaponData;

        private int m_CurrentNumberBullets;

        private IMoving m_Moving;

        public override void Init()
        {
            m_Pool = new PoolBehaviour<Projectile>();
            m_Pool.CreateParent(transform);
            m_Pool.Pooling(m_ProjectileWeaponData.ProjectilePref, 2);
            m_Moving = new ProjectileMovement();
            m_CurrentNumberBullets = m_ProjectileWeaponData.MagazineSize;
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
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

                m_CurrentNumberBullets--;
                m_ShootParticle.Play();

                if (m_CurrentNumberBullets == 0)
                {
                    m_CurrentNumberBullets = m_ProjectileWeaponData.MagazineSize;
                    yield return new WaitForSeconds(m_ProjectileWeaponData.RechargeTime);
                }

                yield return new WaitForSeconds(m_ProjectileWeaponData.AttackSpeed);
            }
        }

        private void OnHit(Projectile projectile)
        {
            m_Pool.ReturnToPool(projectile);
        }
    }
}
