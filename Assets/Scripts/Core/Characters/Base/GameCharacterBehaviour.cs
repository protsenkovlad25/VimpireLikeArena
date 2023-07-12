using System;
using UnityEngine;
using VampireLike.Core.Weapons;

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

        protected IMoving m_Moving;
        protected ILooking m_Looking;
        protected CharacterWeapon m_CharacterWeapon;

        public CharacterData CharacterData => m_CharacterData;
        public CharacterWeapon CharacterWeapon => m_CharacterWeapon;

        public void SetCharacterData(CharacterData characterData)
        {
            m_CharacterData = characterData;
        }

        public virtual void SetCharacterMovement(IMoving moving)
        {
            m_Moving = moving;
        }

        public virtual void SetCharacterLook(ILooking looking)
        {
            m_Looking = looking;
        }

        public virtual void Init()
        {
            m_CurrentHealthPoint = m_CharacterData.HealthPoints;
            m_HpBar = Instantiate(hpBarPrefab, transform).GetComponent<HpBar>();
            m_HpBar.Init(m_CharacterData.HealthPoints);
        }

        public void Push(Vector3 direction ,float force, ForceMode mode = ForceMode.Force)
        {
            if (TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.AddForce(direction * force, mode);
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

        private void Update()
        {
            if (m_CharacterWeapon != null)
            {
                foreach (var weapon in m_CharacterWeapon.Weapons)
                {
                    weapon.Shoot(m_CharacterData.BaseDamage);
                }
            }
        }
    }
}