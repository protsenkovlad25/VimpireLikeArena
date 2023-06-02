using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public class TangentWeapon : WeaponBehaviour
    {
        public override void Init()
        {
            
        }

        public override void Shoot()
        {
            
        }

        public override void Stop()
        {
            
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<ITakingDamage>(out var takingDamage))
            {
                takingDamage.TakeDamage(m_WeaponData.Damage);
            }

            if (collision.gameObject.TryGetComponent<IRepelled>(out var repelled))
            {
                repelled.Push(transform.forward, m_WeaponData.RepulsiveForce);
            }
        }


    }
}