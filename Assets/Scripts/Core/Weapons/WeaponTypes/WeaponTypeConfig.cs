using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    [CreateAssetMenu(fileName = "WeaponTypeData Config", menuName = "Configs/Weapons/WeaponTypeDataConfig")]
    public class WeaponTypeConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponTypeDataConfig> m_WeaponTypeDataHolder;

        public List<WeaponTypeDataConfig> WeaponTypeDataHolders => m_WeaponTypeDataHolder; 
    }

    [System.Serializable]
    public class WeaponTypeDataConfig
    {
        [SerializeField] private string m_Name;

        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private int m_Damage;
        [SerializeField] private float m_FireRate;
        [SerializeField] private float m_FlyTime;
        [SerializeField] private float m_RepulsiveForce;
        [SerializeField] private float m_ProjectileSpeed;

        public string Name => m_Name;

        public WeaponType WeaponType => m_WeaponType;
        public int Damage => m_Damage;
        public float FireRate => m_FireRate;
        public float FlyTime => m_FlyTime;
        public float RepulsiveForce => m_RepulsiveForce;
        public float ProjectileSpeed => m_ProjectileSpeed;
    }
}
