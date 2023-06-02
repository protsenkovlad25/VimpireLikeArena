using System.Collections;
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
        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private float m_AttackSpeed;
        [SerializeField] private float m_RepulsiveForce;
        [SerializeField] private int m_Damage;

        [Space]
        [Header("Projectile Weapon")]
        [SerializeField] private ProjectileType m_ProjectileType;
        [SerializeField] private float m_ProjectileSpeed;
        [SerializeField] private float m_Distance;

        public string Name => m_Name;

        public WeaponType WeaponType => m_WeaponType;
        public float AttackSpeed => m_AttackSpeed;
        public float RepulsiveForce => m_RepulsiveForce;
        public int Damage => m_Damage;

        public ProjectileType ProjectileType => m_ProjectileType;
        public float ProjectileSpeed => m_ProjectileSpeed;
        public float Distance => m_Distance;
    }

    [System.Serializable]
    public class ListModelWeapon
    {
        [SerializeField] private string m_Name;
        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private WeaponBehaviour m_WeaponBehaviour;

        public string Name => m_Name;
        public WeaponType WeaponType => m_WeaponType;
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