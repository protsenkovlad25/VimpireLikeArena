using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Weapons
{
    public class PushingWeapon : WeaponBehaviour
    {
        public override void Init()
        {
        }

        public override void Shoot()
        {
        }

        public override void StopShoot()
        {
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IRepelled>(out var repelled))
            {
                repelled.Push(transform.forward, m_WeaponData.RepulsiveForce);
            }
        }
    }
}
