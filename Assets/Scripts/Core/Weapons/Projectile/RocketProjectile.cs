using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VampireLike.Core.Movements;
using VampireLike.Core.Characters;

namespace VampireLike.Core.Weapons
{
    public class RocketProjectile : Projectile
    {
        [SerializeField] private GameObject m_ExplosionPrefab;

        private Vector3 m_Target;
        private Explosion m_Explosion;

        public void Init()
        {
            ((RocketProjectileMovement)m_Moving).OnRocketMoveEnd = RocketMoveEnd;
        }

        void RocketMoveEnd()
        {
            StartCoroutine(Explosion());

            //gameObject.SetActive(false);
        }

        public override void Move(float speed, Vector3 point, float distance)
        {
            m_Target = point;

            base.Move(speed, point, distance);
        }

        private void Update()
        {
            if (!m_IsMove)
            {
                //gameObject.SetActive(false);
                return;
            }

            var step = m_Speed * Time.deltaTime;
            var oldPostion = transform.position;

            m_Moving.Move(m_Target, step, transform, gameObject.GetComponent<Rigidbody>());

            if (Vector3.Distance(m_StartPosition, transform.position) >= m_Distance)
            {
                m_IsMove = false;
                gameObject.SetActive(false);
            }

            if (Vector3.Distance(oldPostion, transform.position) <= float.Epsilon)
            {
                m_IsMove = false;
                gameObject.SetActive(false);
            }
        }

        private IEnumerator Explosion()
        {
            m_Explosion = Instantiate(m_ExplosionPrefab, transform).GetComponent<Explosion>();
            m_Explosion.PlayParticleExplosion();

            for (int i = 0; i < 4; i++)
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, m_Explosion.CurrentExpDistance * (i + 1), new Vector3(0, 1, 0), .1F);

                foreach(var hit in hits)
                {
                    if(hit.collider.gameObject.TryGetComponent(out GameCharacterBehaviour gameCharacterBehaviour))
                    {
                        hit.collider.gameObject.TryGetComponent<IRepelled>(out var repelled);
                        {
                            repelled.Push(transform.forward, RepulsiveForce);
                        }
                        //if (hit.collider.gameObject.TryGetComponent<IRepelled>(out var repelled))
                        //{
                        //    repelled.Push(transform.forward, RepulsiveForce);
                        //}

                        if (gameCharacterBehaviour.GetType() == typeof(MainCharacter))
                        {
                            hit.collider.gameObject.TryGetComponent<ITakingDamage>(out var takingDamage);
                            {
                                takingDamage.TakeDamage(Damage * (int)(1 - i * 0.25));
                            }
                        }
                    }
                }

                yield return new WaitForSeconds(0.2f);
            }

            gameObject.SetActive(false);
        }
    }
}
