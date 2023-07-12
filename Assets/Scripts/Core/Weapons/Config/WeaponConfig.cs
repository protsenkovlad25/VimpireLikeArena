using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    [CreateAssetMenu(fileName = "Weapon Config", menuName = "Configs/Weapons/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponDataConfig> m_WeaponData;
        [Space]
        [SerializeField] private List<ListModelWeapon> m_ListModelWeapons;
        [Space]
        [SerializeField] private List<ListModelProjectile> m_ListModelProjectile;


        public List<WeaponDataConfig> WeaponData => m_WeaponData;

        public List<ListModelWeapon> ListModelWeapons => m_ListModelWeapons;

        public List<ListModelProjectile> ListModelProjectile => m_ListModelProjectile;
    }

    [System.Serializable]
    public class WeaponDataConfig
    {
        [SerializeField] private string m_Name;

        [Header("General")]
        [SerializeField] private WeaponVariant m_WeaponVariant;
        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private WeaponClass m_WeaponClass;
        [SerializeField] private float m_FireRate;
        [SerializeField] private float m_RepulsiveForce;
        [SerializeField] private int m_Damage;
        [SerializeField] private int m_MagazineSize;
        [SerializeField] private float m_RechargeTime;

        [Space]
        [Header("Projectile Weapon")]
        [SerializeField] private ProjectileType m_ProjectileType;
        [SerializeField] private float m_ProjectileSpeed;
        [SerializeField] private float m_FlyTime;

        public string Name => m_Name;

        public WeaponVariant WeaponVariant => m_WeaponVariant;
        public WeaponType WeaponType => m_WeaponType;
        public WeaponClass WeaponClass => m_WeaponClass;
        public float FireRate => m_FireRate;
        public float RepulsiveForce => m_RepulsiveForce;
        public int Damage => m_Damage;
        public int MagazineSize => m_MagazineSize;
        public float RechargeTime => m_RechargeTime;

        public ProjectileType ProjectileType => m_ProjectileType;
        public float ProjectileSpeed => m_ProjectileSpeed;
        public float FlyTime => m_FlyTime;
    }

    [System.Serializable]
    public class ListModelWeapon
    {
        [SerializeField] private string m_Name;
        [SerializeField] private WeaponVariant m_WeaponVariant;
        [SerializeField] private WeaponBehaviour m_WeaponBehaviour;

        public string Name => m_Name;
        public WeaponVariant WeaponVariant => m_WeaponVariant;
        public WeaponBehaviour WeaponBehaviour => m_WeaponBehaviour;
    }

    [System.Serializable]
    public class ListModelProjectile
    {
        [SerializeField] private string m_Name;
        [SerializeField] private ProjectileType m_ProjectileType;
        [SerializeField] private Projectile m_Projectile;

        public string Name => m_Name;
        public ProjectileType ProjectileType => m_ProjectileType;
        public Projectile Projectile => m_Projectile;
    }
}