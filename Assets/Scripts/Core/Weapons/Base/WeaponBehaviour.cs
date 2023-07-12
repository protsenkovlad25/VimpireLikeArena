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
        protected WeaponTypeDataHolder m_WeaponTypeData;
        protected WeaponClassDataHolder m_WeaponClassData;
        protected PoolBehaviour<Projectile> m_Pool;

        protected int m_CurrentNumberBullets;
        protected float m_Delay = 0;
        protected bool m_IsShoot;

        protected IMoving m_Moving;
        protected IAttaching m_Attaching;

        public WeaponType WeaponType => m_WeaponTypeData.WeaponType;
        public WeaponClass WeaponClass => m_WeaponClassData.WeaponClass;

        public virtual void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public virtual void SetType(WeaponTypeDataHolder weaponTypeData)
        {
            m_WeaponTypeData = weaponTypeData;
        }

        public virtual void SetClass(WeaponClassDataHolder weaponClassData)
        {
            m_WeaponClassData = weaponClassData;
        }

        public virtual void Init()
        {
            m_Pool = new PoolBehaviour<Projectile>();
            m_Pool.CreateParent(transform);
            m_Pool.Pooling(m_WeaponData.ProjectilePref, 20);

            m_Moving = new ProjectileMovement();

            m_CurrentNumberBullets = m_WeaponData.MagazineSize;
        }

        public virtual void Shoot(int baseDamage)
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

                        projectile.Damage = baseDamage * GetWeaponDamage();
                        projectile.RepulsiveForce = GetWeaponRepulsiveForce();
                        
                        projectile.SetMovement(m_Moving);
                        projectile.Move(GetWeaponProjectileSpeed(), m_Attaching.GetTarget().position, GetWeaponFlyTime());

                        if (m_CurrentNumberBullets != 0)
                            m_Delay = GetWeaponFireRate();
                        else
                            m_Delay = GetWeaponFireRate() + m_WeaponData.RechargeTime;
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

        public virtual WeaponData GetWeaponData()
        {
            return m_WeaponData;
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

        #region GetWeaponDataProperty
        public virtual int GetWeaponDamage()
        {
            return m_WeaponData.Damage * m_WeaponTypeData.Damage * m_WeaponClassData.Damage;
        }

        public virtual float GetWeaponRepulsiveForce()
        {
            return m_WeaponData.RepulsiveForce * m_WeaponTypeData.RepulsiveForce * m_WeaponClassData.RepulsiveForce;
        }

        public virtual float GetWeaponProjectileSpeed()
        {
            return m_WeaponData.ProjectileSpeed * m_WeaponTypeData.ProjectileSpeed * m_WeaponClassData.ProjectileSpeed;
        }

        public virtual float GetWeaponFlyTime()
        {
            return m_WeaponData.FlyTime * m_WeaponTypeData.FlyTime * m_WeaponClassData.FlyTime;
        }

        public virtual float GetWeaponFireRate()
        {
            return m_WeaponData.FireRate * m_WeaponTypeData.FireRate * m_WeaponClassData.FireRate;
        }
        #endregion
    }
}