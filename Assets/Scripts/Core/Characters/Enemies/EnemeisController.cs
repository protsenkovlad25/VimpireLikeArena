using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VampireLike.Core.Characters.Enemies.Config;
using VampireLike.Core.Movements;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters.Enemies
{
    public class EnemeisController : MonoBehaviour, IIniting, IAttaching
    {
        public event Action OnAllDeadEnemies;

        [SerializeField] private EnemyConfigurator m_EnemyConfigurator;

        [SerializeField] private List<EnemyCharacter> m_Enemies;
        [SerializeField] private bool m_IsMove;

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
            m_Enemies.Remove(characterBehaviour.GetComponent<EnemyCharacter>());


            if (m_Enemies.Count == 0)
            {
                OnAllDeadEnemies?.Invoke();
            }
        }

        public void SetEnemies(List<EnemyCharacter> enemies)
        {
            //m_Enemies = enemies;
            foreach (var en in enemies)
            {
                m_Enemies.Add(en);
            }
        }

        public List<EnemyCharacter> GetEnemies()
        {
            return m_Enemies;
        }

        public void InitEnemy()
        {
            foreach (var enemy in m_Enemies)
            {
                enemy.SetCharacterData(m_EnemyConfigurator.GetData(enemy.GetEnemyType()));
                enemy.SetCharacterMovement(m_EnemyConfigurator.GetMovement(enemy.GetEnemyType()));
                enemy.Set(m_Attaching);
                enemy.transform.position += new Vector3(0, 50, 0);
                enemy.Init();
                enemy.OnDie += OnEnemyDie;
            }
        }

        public void Landing()
        {
            foreach (var enemy in m_Enemies)
            {
                enemy.transform.DOMoveY(1.6f, .8f).SetEase(Ease.OutCubic);
            }
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