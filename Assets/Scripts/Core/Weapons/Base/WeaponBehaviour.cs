using UnityEngine;
using VampireLike.General;

namespace VampireLike.Core.Weapons
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] protected Transform m_StartPoint;
        [SerializeField] protected ParticleSystem m_ShootParticle;

        [Header("Weapon Data")]
        [SerializeField] protected WeaponType m_WeaponType;
        [SerializeField] protected WeaponClass m_WeaponClass;
        [SerializeField] protected int m_Damage;
        [SerializeField] protected int m_MagazineSize;
        [SerializeField] protected float m_FlyTime;
        [SerializeField] protected float m_FireRate;
        [SerializeField] protected float m_RechargeTime;
        [SerializeField] protected float m_RepulsiveForce;
        [SerializeField] protected float m_ProjectileSpeed;
        [SerializeField] protected Projectile m_ProjectilePref;
        #endregion

        #region Fields
        protected WeaponData m_WeaponData;
        protected WeaponTypeDataHolder m_WeaponTypeData;
        protected WeaponClassDataHolder m_WeaponClassData;
        protected PoolBehaviour<Projectile> m_Pool;

        protected int m_CurrentNumberBullets;
        protected float m_Delay = 0;
        protected bool m_IsShoot;

        protected IMoving m_Moving;
        protected IAttaching m_Attaching;
        #endregion

        #region Properties
        public WeaponType WeaponType => m_WeaponType;
        public WeaponClass WeaponClass => m_WeaponClass;

        public int Damage
        {
            get => m_Damage;
            set => m_Damage = value;
        }
        public int MagazineSize
        {
            get => m_MagazineSize;
            set => m_MagazineSize = value;
        }
        public float FlyTime
        {
            get => m_FlyTime;
            set => m_FlyTime = value;
        }
        public float FireRate
        {
            get => m_FireRate;
            set => m_FireRate = value;
        }
        public float RechargeTime
        {
            get => m_RechargeTime;
            set => m_RechargeTime = value;
        }
        public float RepulsiveForce
        {
            get => m_RepulsiveForce;
            set => m_RepulsiveForce = value;
        }
        public float ProjectileSpeed
        {
            get => m_ProjectileSpeed;
            set => m_ProjectileSpeed = value;
        }
        #endregion

        #region Methods
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
            m_Pool.Pooling(m_ProjectilePref, 20);

            m_Moving = new ProjectileMovement();

            m_CurrentNumberBullets = m_MagazineSize;
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
                            m_CurrentNumberBullets = m_MagazineSize;

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
                            m_Delay = GetWeaponFireRate() + m_RechargeTime;
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
            return m_Damage * m_WeaponTypeData.Damage * m_WeaponClassData.Damage;
        }

        public virtual float GetWeaponRepulsiveForce()
        {
            return m_RepulsiveForce * m_WeaponTypeData.RepulsiveForce * m_WeaponClassData.RepulsiveForce;
        }

        public virtual float GetWeaponProjectileSpeed()
        {
            return m_ProjectileSpeed * m_WeaponTypeData.ProjectileSpeed * m_WeaponClassData.ProjectileSpeed;
        }

        public virtual float GetWeaponFlyTime()
        {
            return m_FlyTime * m_WeaponTypeData.FlyTime * m_WeaponClassData.FlyTime;
        }

        public virtual float GetWeaponFireRate()
        {
            return m_FireRate * m_WeaponTypeData.FireRate * m_WeaponClassData.FireRate;
        }
        #endregion
        #endregion
    }
}