using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public class PushingWeapon : WeaponBehaviour
    {
        public override void Init()
        {
        }

        public override void Shoot(int baseDamage)
        {
        }

        public override void StopShoot()
        {
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IRepelled>(out var repelled))
            {
                repelled.Push(transform.forward, m_RepulsiveForce);
            }
        }
    }
}
