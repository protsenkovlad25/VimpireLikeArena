using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public class WeaponData
    {
        public float FireRate { get; set; }
        public int Damage { get; set; }
        public float RepulsiveForce { get; set; }
        public int MagazineSize { get; set; }
        public float RechargeTime { get; set; }
        public float ProjectileSpeed { get; set; }
        public float FlyTime { get; set; }
        public Projectile ProjectilePref { get; set; }
    }
}