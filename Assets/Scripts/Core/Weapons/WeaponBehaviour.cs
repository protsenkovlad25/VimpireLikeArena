using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Movements;

namespace VampireLike.Core.Weapons
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] protected Transform m_StartPoint;
        [SerializeField] protected ParticleSystem m_ShootParticle;

        protected WeaponData m_WeaponData;
        protected PoolBehaviour<Projectile> m_Pool;

        protected int m_CurrentNumberBullets;
        protected float m_Delay = 0;
        protected bool m_IsShoot;

        protected IMoving m_Moving;
        protected IAttaching m_Attaching;

        public virtual void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public virtual void Init()
        {
            m_Pool = new PoolBehaviour<Projectile>();
            m_Pool.CreateParent(transform);
            m_Pool.Pooling(m_WeaponData.ProjectilePref, 20);

            m_Moving = new ProjectileMovement();

            m_CurrentNumberBullets = m_WeaponData.MagazineSize;
        }

        public virtual void Shoot()
        {
            if (m_Delay <= 0)
            {
                if (m_Attaching.GetTarget() != null)
                {
                    if (m_IsShoot)
                    {
                        if (m_CurrentNumberBullets == 0) 
                            m_CurrentNumberBullets = m_WeaponData.MagazineSize;

                        m_CurrentNumberBullets--;

                        var projectile = m_Pool.Take();
                        projectile.transform.SetParent(null);
                        projectile.gameObject.layer = gameObject.layer;
                        projectile.transform.position = m_StartPoint.position;
                        projectile.Damage = m_WeaponData.Damage;
                        projectile.RepulsiveForce = m_WeaponData.RepulsiveForce;
                        projectile.SetMovement(m_Moving);
                        projectile.Move(m_WeaponData.ProjectileSpeed, m_Attaching.GetTarget().position, m_WeaponData.FlyTime);

                        if (m_CurrentNumberBullets != 0)
                            m_Delay = m_WeaponData.FireRate;
                        else
                            m_Delay = m_WeaponData.FireRate + m_WeaponData.RechargeTime;
                    }
                }
            }
            else
            {
                m_Delay -= Time.deltaTime;
            }
        }

        public virtual void StopShoot()
        {
            m_IsShoot = false;
            m_Pool.ReturnAllPull();
        }

        public virtual void StartShoot()
        {
            m_IsShoot = true;
        }

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