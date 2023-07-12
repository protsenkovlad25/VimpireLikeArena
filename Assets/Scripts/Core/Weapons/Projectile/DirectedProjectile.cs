using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public class DirectedProjectile : Projectile
    {
        public override void Move(float speed, Vector3 point, float flyTime)
        {
            base.Move(speed, point, flyTime);
        }

    }
}