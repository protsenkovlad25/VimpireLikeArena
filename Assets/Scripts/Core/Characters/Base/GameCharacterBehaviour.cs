using System;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters
{
    public abstract class GameCharacterBehaviour : MonoBehaviour, ITakingDamage, IRepelled, IIniting
    {
        #region Actions
        public event Action<GameCharacterBehaviour> OnTakeDamage;
        public event Action<GameCharacterBehaviour> OnDie;
        #endregion

        #region SerializeFields
        [SerializeField] protected List<Transform> m_WeaponPoints;
        [SerializeField] protected List<GameObject> m_WeaponPrefabs;
        [SerializeField] protected GameObject m_HpBarPrefab;
        #endregion

        #region Fields
        private int m_CurrentHealthPoint;
        private int m_StartedHealthPoint;

        private HpBar m_HpBar;

        protected IMoving m_Moving;
        protected ILooking m_Looking;
        protected IAttaching m_Attaching;
        protected CharacterData m_CharacterData;
        protected CharacterWeapon m_CharacterWeapon;
        protected List<GameObject> m_WeaponObjects;
        #endregion

        #region Properties
        public int CurrentHealthPoint => m_CurrentHealthPoint;
        public int StartedHealthPoint => m_StartedHealthPoint;
        public CharacterData CharacterData => m_CharacterData;
        public CharacterWeapon CharacterWeapon => m_CharacterWeapon;
        #endregion

        #region Methods
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
            m_WeaponObjects = new List<GameObject>();
            for (int i = 0; i < m_WeaponPoints.Count; i++)
                m_WeaponObjects.Add(null);

            m_CharacterWeapon = new CharacterWeapon();
            m_CharacterWeapon.Set(m_Attaching);

            m_CurrentHealthPoint = m_CharacterData.HealthPoints;
            m_StartedHealthPoint = m_CharacterData.HealthPoints;

            m_HpBar = Instantiate(m_HpBarPrefab, transform).GetComponent<HpBar>();
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
        #endregion
    }
}