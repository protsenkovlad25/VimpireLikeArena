using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;

namespace VampireLike.Core.Levels
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float m_CurrentExpDistance;
        [SerializeField] private float m_RepulsiveForce;
        [SerializeField] private ParticleSystem m_SmokeParticle;
        [SerializeField] private ParticleSystem m_SparksParticle;

        public float CurrentExpDistance => m_CurrentExpDistance;
        public float RepulsiveForce => m_RepulsiveForce;

        public void Init(Vector3 target, Transform rocketTransform, int damage)
        {
            StartCoroutine(InitializeExplosion(target, rocketTransform, damage));
            PlayParticleExplosion();
        }

        private IEnumerator InitializeExplosion(Vector3 target, Transform rocketTransform, int damage)
        {
            List<GameObject> exceptions = new List<GameObject>();
            for (int i = 0; i < 4; i++)
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, CurrentExpDistance * (i + 1f), new Vector3(0, 1, 0), .1f);

                foreach (var hit in hits)
                {
                    if (!exceptions.Contains(hit.collider.gameObject))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out GameCharacterBehaviour gameCharacterBehaviour))
                        {
                            Vector3 pushDir = (transform.position - target).normalized;
                            hit.collider.gameObject.TryGetComponent<IRepelled>(out var repelled);
                            {
                                if (pushDir.magnitude <= .1f)
                                    pushDir = rocketTransform.forward.normalized;

                                repelled.Push(pushDir, RepulsiveForce * (1F - i * 0.25f), ForceMode.Impulse);
                            }

                            if (gameCharacterBehaviour.GetType() == typeof(MainCharacter))
                            {
                                hit.collider.gameObject.TryGetComponent<ITakingDamage>(out var takingDamage);
                                {
                                    takingDamage.TakeDamage((int)(damage * (1f - i * 0.25f)));
                                }
                            }
                            exceptions.Add(hit.collider.gameObject);
                        }
                    }
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

        public void PlayParticleExplosion()
        {
            m_SmokeParticle.Play();
            m_SparksParticle.Play();
        }
    }
}
