using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VampireLike.Core.Characters;
using VampireLike.Core.General;

namespace VampireLike.Core.Weapons
{
    public class RocketShootingWeapon : WeaponBehaviour, INeeding<IAttaching>
    {
        [SerializeField] private List<RocketProjectile> m_RocketProjectiles;
 
        private EnemyCharacter m_EnemyCharacter;

        private int m_Dir = -1;

        public override void Init()
        {
            m_Moving = new RocketProjectileMovement();
            m_Delay = m_FireRate;
        }

        public void SetEnemyCharacter(EnemyCharacter enemyCharacter)
        {
            m_EnemyCharacter = enemyCharacter;
        }

        public override void Shoot(int baseDamage)
        {
            if (m_Delay <= 0)
            {
                if (m_Attaching.GetTarget() != null)
                {
                    if (m_IsShoot)
                    {
                        if (m_RocketProjectiles != null)
                        {
                            m_RocketProjectiles[0].SetMovement(m_Moving);
                            m_RocketProjectiles[0].Init();

                            m_RocketProjectiles[0].Damage = baseDamage * GetWeaponDamage();
                            m_RocketProjectiles[0].RepulsiveForce = GetWeaponRepulsiveForce();

                            m_RocketProjectiles[0].transform.DOMove(transform.position + m_RocketProjectiles[0].transform.right * m_Dir * 3, .2f).SetEase(Ease.InExpo);
                            m_Dir = -m_Dir;

                            m_RocketProjectiles[0].Move(GetWeaponProjectileSpeed(), m_Attaching.GetTarget().position, GetWeaponFlyTime());

                            m_Delay = GetWeaponFireRate() + m_RechargeTime;

                            if (m_RocketProjectiles.Count == 1)
                            {
                                m_RocketProjectiles = null;
                                m_Delay = 0;
                            }
                            else
                                m_RocketProjectiles.RemoveAt(0);
                        }
                        else
                        {
                            StopShoot();
                            SwitchState();
                        }
                    }
                }
            }
            else
            {
                m_Delay -= Time.deltaTime;
            }
        }

        public override void StopShoot()
        {
            m_IsShoot = false;
        }

        private void SwitchState()
        {
            EventManager.SwitchMovement(m_EnemyCharacter, new DashMovement());
            EventManager.SwitchLook(m_EnemyCharacter, new SimpleLook());
            EventManager.SwitchWeapon(WeaponVariant.Pushing, m_EnemyCharacter);
            Destroy(this);
        }
    }
}
