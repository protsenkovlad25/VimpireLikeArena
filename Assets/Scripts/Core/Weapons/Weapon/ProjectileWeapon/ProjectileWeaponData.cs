using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{
    [System.Serializable]
    public class ProjectileWeaponData : WeaponData
    {
        public Projectile ProjectilePref { get; set; }
        public float ProjectileSpeed { get; set; }
        public float Distance { get; set; }
    }
}