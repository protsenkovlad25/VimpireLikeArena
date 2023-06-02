using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Characters
{

    public abstract class GameCharacterBehaviour : MonoBehaviour, ITakingDamage, IRepelled, IIniting
    {
        [SerializeField] protected GameObject hpBarPrefab;

        public event Action<GameCharacterBehaviour> OnTakeDamage;
        public event Action<GameCharacterBehaviour> OnDie;

        private int m_CurrentHealthPoint;
        public int CurrentHealthPoint => m_CurrentHealthPoint;

        private CharacterData m_CharacterData;
        private HpBar m_HpBar;

        public CharacterData CharacterData => m_CharacterData;

        protected IMoving m_Moving;

        public void SetCharacterData(CharacterData characterData)
        {
            m_CharacterData = characterData;
        }

        public void SetCharacterMovement(IMoving moving)
        {
            m_Moving = moving;
        }

        public virtual void Init()
        {
            m_CurrentHealthPoint = m_CharacterData.HealthPoints;
            m_HpBar = Instantiate(hpBarPrefab, transform).GetComponent<HpBar>();
            m_HpBar.Init(m_CharacterData.HealthPoints);
        }

        public void Push(Vector3 direction ,float force)
        {
            if (TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.AddForce(direction * force, ForceMode.Force);
            }
        }

        public virtual void TakeDamage(int damage)
        {
            m_CurrentHealthPoint -= damage;
            m_HpBar.UpdateHp(m_CurrentHealthPoint);
            OnTakeDamage?.Invoke(this);

            if (m_CurrentHealthPoint <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log(gameObject.name + " - I dead.");
            OnDie?.Invoke(this);
        }
    }
}