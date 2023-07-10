using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    [CreateAssetMenu(fileName = "WeaponClassData Config", menuName = "Configs/Weapons/WeaponClassDataConfig")]
    public class WeaponClassConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponClassDataConfig> m_WeaponClassDataHolder;

        public List<WeaponClassDataConfig> WeaponClassDataHolders => m_WeaponClassDataHolder;
    }

    [System.Serializable]
    public class WeaponClassDataConfig
    {
        [SerializeField] private string m_Name;

        [SerializeField] private WeaponClass m_WeaponClass;
        [SerializeField] private int m_Damage;
        [SerializeField] private float m_FireRate;
        [SerializeField] private float m_FlyTime;
        [SerializeField] private float m_RepulsiveForce;
        [SerializeField] private float m_ProjectileSpeed;

        public string Name => m_Name;

        public WeaponClass WeaponClass => m_WeaponClass;
        public int Damage => m_Damage;
        public float FireRate => m_FireRate;
        public float FlyTime => m_FlyTime;
        public float RepulsiveForce => m_RepulsiveForce;
        public float ProjectileSpeed => m_ProjectileSpeed;
    }
}
