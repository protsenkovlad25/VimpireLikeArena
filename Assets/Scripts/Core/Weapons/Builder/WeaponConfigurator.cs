using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public class WeaponConfigurator : MonoBehaviour
    {
        [SerializeField] private WeaponConfig m_WeaponConfig;
        [SerializeField] private WeaponTypeConfig m_WeaponTypeConfig;
        [SerializeField] private WeaponClassConfig m_WeaponClassConfig;

        public WeaponDTO GetData(WeaponVariant weaponVariant)
        {
            WeaponDataConfig data = m_WeaponConfig.WeaponData.Find(item => item.WeaponVariant.Equals(weaponVariant));

            ListModelWeapon prefab = m_WeaponConfig.ListModelWeapons.Find(item => item.WeaponVariant.Equals(weaponVariant));

            ListModelProjectile projectile = m_WeaponConfig.ListModelProjectile.Find(item => item.ProjectileType.Equals(data.ProjectileType));

            var dto = new WeaponDTO();
            dto.WeaponData = new WeaponData
            {
                Damage = data.Damage,
                FireRate = data.FireRate,
                RepulsiveForce = data.RepulsiveForce,
                MagazineSize = data.MagazineSize,
                RechargeTime = data.RechargeTime,
                ProjectileSpeed = data.ProjectileSpeed,
                FlyTime = data.FlyTime,
                ProjectilePref = projectile.Projectile,
                WeaponType = data.WeaponType,
                WeaponClass = data.WeaponClass
            };

            dto.WeaponBehaviour = prefab.WeaponBehaviour;
            return dto;
        }

        public List<WeaponTypeDataHolder> GetTypeDataHolders()
        {
            List<WeaponTypeDataHolder> typeDataHolders = new List<WeaponTypeDataHolder>();

            foreach (var typeData in m_WeaponTypeConfig.WeaponTypeDataHolders)
                typeDataHolders.Add(new WeaponTypeDataHolder 
                {
                    WeaponType = typeData.WeaponType,
                    Damage = typeData.Damage,
                    FireRate = typeData.FireRate,
                    FlyTime = typeData.FlyTime,
                    ProjectileSpeed = typeData.ProjectileSpeed,
                    RepulsiveForce = typeData.RepulsiveForce
                });

            return typeDataHolders;
        }

        public List<WeaponClassDataHolder> GetClassDataHolders()
        {
            List<WeaponClassDataHolder> classDataHolders = new List<WeaponClassDataHolder>();

            foreach (var classData in m_WeaponClassConfig.WeaponClassDataHolders)
                classDataHolders.Add(new WeaponClassDataHolder
                {
                    WeaponClass = classData.WeaponClass,
                    Damage = classData.Damage,
                    FireRate = classData.FireRate,
                    FlyTime = classData.FlyTime,
                    ProjectileSpeed = classData.ProjectileSpeed,
                    RepulsiveForce = classData.RepulsiveForce
                });

            return classDataHolders;
        }
    }
}