using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VampireLike.Core.Characters.Enemies.Config;
using VampireLike.Core.Looks;
using VampireLike.Core.Movements;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters.Enemies
{
    public class EnemeisController : MonoBehaviour, IIniting, IAttaching
    {
        public event Action OnAllDeadEnemies;

        [SerializeField] private EnemyConfigurator m_EnemyConfigurator;
        [SerializeField] private GameObject m_MarkPrefab;
        [SerializeField] private GameObject m_SpawnParticlePrefab;
        [SerializeField] private GameObject m_DeathParticlePrefab;

        [SerializeField] private List<EnemyCharacter> m_Enemies;
        [SerializeField] private bool m_IsMove;

        private List<GameObject> m_Marks;
        private SpawnParticle m_SpawnParticle;
        private DeathParticle m_DeathParticle;

        private IAttaching m_Attaching;

        public IEnumerable<INeedingWeapon> NeedingWeapons => m_Enemies.OfType<INeedingWeapon>();

        public void Attach()
        {
            if (m_Attaching == null)
            {
                Debug.LogError($"Class - {nameof(EnemeisController)}:\n" +
                               $"Method - {nameof(Attach)}. Null References - {nameof(m_Attaching)}.");
                return;
            }

            if (m_Enemies == null)
            {
                Debug.LogError($"Class - {nameof(EnemeisController)}:\n" +
                               $"Method - {nameof(Attach)}. Null References - {nameof(m_Enemies)}.");
                return;
            }

            if (!m_IsMove)
            {
                return;
            }

            foreach (var enemy in m_Enemies)
            {
                enemy.Move(m_Attaching);
            }
        }

        public void StartShoot()
        {
            foreach (var item in m_Enemies)
            {
                item.StartShoot();
            }
        }

        public void StopShoot()
        {
            foreach (var item in m_Enemies)
            {
                item.StopShoot();
            }
        }

        public Transform GetTarget()
        {
            if (m_Enemies.Count == 0)
            {
                return null;
            }

            Transform enemyCharacter = null;

            var position = m_Attaching.GetTarget().position;
            float distace = Vector3.Distance(position, m_Enemies[0].transform.position);

            foreach (var enemy in m_Enemies)
            {
                if (enemy == null || enemy.transform == null)
                {
                    continue;
                }

                var ray = new Ray(position, enemy.transform.position);

                float calculateDistance = Vector3.Distance(position, enemy.transform.position);
                if (calculateDistance <= distace)
                {
                    distace = calculateDistance;
                    enemyCharacter = enemy.transform;
                }
            }

            return enemyCharacter;
        }

        public void Init()
        {
            m_Marks = new List<GameObject>();
            EventManager.OnSwitchMovement.AddListener(SwitchMovement);
        }

        public void SetAttaching(IAttaching attaching)
        {
            if (attaching == null)
            {
                Debug.LogError($"Class - {nameof(EnemeisController)}:\n" +
                               $"Method - {nameof(SetAttaching)}. Null References - {nameof(attaching)}.");
                return;
            }

            m_Attaching = attaching;
            foreach (var item in m_Enemies)
            {
                item.Set(m_Attaching);
            }
        }

        public void OnEnemyDie(GameCharacterBehaviour characterBehaviour)
        {
            Debug.LogError("Die");
            characterBehaviour.OnDie -= OnEnemyDie;
            characterBehaviour.gameObject.SetActive(false);
            PlayDeathParticle(characterBehaviour.gameObject.transform.position);
            m_Enemies.Remove(characterBehaviour.GetComponent<EnemyCharacter>());


            if (m_Enemies.Count == 0)
            {
                OnAllDeadEnemies?.Invoke();
            }
        }

        public void SetEnemies(List<EnemyCharacter> enemies)
        {
            //m_Enemies = enemies;
            foreach (var enemy in enemies)
            {
                m_Enemies.Add(enemy);
            }
        }

        public List<EnemyCharacter> GetEnemies()
        {
            return m_Enemies;
        }

        public void InitEnemy(List<EnemyCharacter> enemies)
        {
            foreach (var enemy in enemies)
            {
                for (int i = 0; i < m_Enemies.Count; i++)
                {
                    if (enemy == m_Enemies[i])
                    {
                        enemy.SetCharacterData(m_EnemyConfigurator.GetData(enemy.GetEnemyType()));
                        enemy.SetCharacterMovement(m_EnemyConfigurator.GetMovement(enemy.GetEnemyType()));
                        enemy.SetCharacterLook(m_EnemyConfigurator.GetLooking(enemy.GetEnemyType()));
                        enemy.Set(m_Attaching);
                        enemy.transform.position += new Vector3(0, 50, 0);
                        enemy.Init();
                        enemy.OnDie += OnEnemyDie;
                        enemy.gameObject.SetActive(false);
                    }
                }
            }
        }

        public void ActivateEnemies(List<EnemyCharacter> enemies)
        {
            foreach (var enemy in enemies)
            {
                for (int i = 0; i < m_Enemies.Count; i++)
                {
                    if (enemy == m_Enemies[i])
                    {
                        enemy.gameObject.SetActive(true);
                    }
                }
            }
        }

        public void SetMark(List<EnemyCharacter> enemies)
        {
            foreach (var enemy in enemies)
            {
                for (int i = 0; i < m_Enemies.Count; i++)
                {
                    if (enemy == m_Enemies[i])
                    {
                        RaycastHit[] hits = Physics.RaycastAll(m_Enemies[i].transform.position, new Vector3(0, -1, 0), 100f);
                        foreach(RaycastHit hit in hits)
                        {
                            if (hit.collider.TryGetComponent(out OnColiderEnterComponent c))
                            {
                                GameObject mark;
                                mark = Instantiate(m_MarkPrefab, hit.point + new Vector3(0, .1f, 0), m_MarkPrefab.transform.rotation);

                                if (mark != null)
                                    m_Marks.Add(mark);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Landing(List<EnemyCharacter> enemies)
        {
            foreach (var enemy in enemies)
            {
                for (int i = 0; i < m_Enemies.Count; i++)
                {
                    if (enemy == m_Enemies[i])
                    {
                        enemy.transform.localScale = new Vector3(.7f, 1.4f, .7f);

                        Sequence s = DOTween.Sequence();
                        s.Append(enemy.transform.DOMoveY(1.6f, .8f).SetEase(Ease.InQuad));
                        s.Insert(.7f, enemy.transform.DOScale(new Vector3(1.2f,.7f,1.2f), .15f));
                        s.Append(enemy.transform.DOScale(1, .2f));
                        StartCoroutine(PlaySpawnParticle(enemy.transform.position));
                    }
                }
            }
        }

        private void PlayDeathParticle(Vector3 position)
        {
            m_DeathParticle = Instantiate(m_DeathParticlePrefab, position, Quaternion.identity).GetComponent<DeathParticle>();
            m_DeathParticle.PlayParticle();

            StartCoroutine(DestroyParticle(m_DeathParticle));
        }

        private IEnumerator PlaySpawnParticle(Vector3 position)
        {
            yield return new WaitForSeconds(0.8f);

            m_SpawnParticle = Instantiate(m_SpawnParticlePrefab, position, Quaternion.identity).GetComponent<SpawnParticle>();
            m_SpawnParticle.PlayParticle();

            StartCoroutine(DestroyParticle(m_SpawnParticle));
        }

        private IEnumerator DestroyParticle(SpawnParticle spawnParticle)
        {
            yield return new WaitForSeconds(1.5f);

            Destroy(spawnParticle.gameObject);
        }

        private IEnumerator DestroyParticle(DeathParticle deathParticle)
        {
            yield return new WaitForSeconds(1.5f);

            Destroy(deathParticle.gameObject);
        }

        public void SwitchMovement(EnemyCharacter enemy, IMoving moving)
        {
            enemy.SetCharacterMovement(moving);
        }

        public void InitEnemeisWeapons()
        {
            foreach (var item in m_Enemies)
            {
                item.InitWeapon();
            }
        }
    }
}