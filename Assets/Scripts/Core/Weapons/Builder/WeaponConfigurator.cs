using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public class WeaponConfigurator : MonoBehaviour
    {
        [SerializeField] private WeaponConfig m_WeaponConfig;

        public WeaponDTO GetData(WeaponVariant weaponVariant)
        {
            var data = m_WeaponConfig.WeaponData.Find(item => item.WeaponType.Equals(weaponVariant));
            var prefab = m_WeaponConfig.ListModelWeapons.Find(item => item.WeaponVariant.Equals(weaponVariant));

            var dto = new WeaponDTO();

            var projectile = m_WeaponConfig.ListModelProjectile.Find(item => item.ProjectileType.Equals(data.ProjectileType));

            dto.WeaponData = new WeaponData
            {
                Damage = data.Damage,
                FireRate = data.FireRate,
                RepulsiveForce = data.RepulsiveForce,
                MagazineSize = data.MagazineSize,
                RechargeTime = data.RechargeTime,
                ProjectileSpeed = data.ProjectileSpeed,
                FlyTime = data.FlyTime,
                ProjectilePref = projectile.Projectile
            };

            dto.WeaponBehaviour = prefab.WeaponBehaviour;
            return dto;
        }
    }
}