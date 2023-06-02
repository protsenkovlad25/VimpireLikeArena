using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters
{
    public class MainCharacterController : MonoBehaviour, IAttaching, IIniting, INeedingWeapon
    {
        [SerializeField] private MainCharacter m_MainCharacter;
        [SerializeField] private WeaponType m_WeaponType;

        private CharacterWeapon m_CharacterWeapon;

        private IAttaching m_Attaching;

        public void SetAttaching(IAttaching attaching)
        {
            if (attaching == null)
            {
                Debug.LogError($"Class - {nameof(MainCharacterController)}:\n" +
                               $"Method - {nameof(SetAttaching)}. Null References - {nameof(attaching)}.");
                return;
            }

            m_Attaching = attaching;
        }

        public Transform GetTarget()
        {
            return m_MainCharacter.transform;
        }

        public void Init()
        {
            m_MainCharacter.SetCharacterMovement(new CharacterMovement());
            m_MainCharacter.SetCharacterData(new CharacterData()
            {
                Speed = 5,
                HealthPoints = 20,
                ScaleDamage = 1
            });
            m_MainCharacter.Init();
            m_CharacterWeapon.Init();
            m_MainCharacter.OnDie += OnMainCharacterDie;
        }

        public void StartShoot()
        {
            m_CharacterWeapon.Start();
        }

        public void StopShoot()
        {
            m_CharacterWeapon.Stop();
        }

        public void Move(Vector2 vector2)
        {
            m_MainCharacter.Move(vector2);
        }

        public WeaponType GetWeaponType()
        {
            return m_WeaponType;
        }

        public Transform Where()
        {
            return m_MainCharacter.WeaponPoint;
        }

        public void Set(WeaponBehaviour generic)
        {
            generic.gameObject.layer = 7;
            if (m_CharacterWeapon == null)
            {
                m_CharacterWeapon = new CharacterWeapon();
                m_CharacterWeapon.Set(m_Attaching);
            }

            m_CharacterWeapon.AddWeapon(generic);
        }

        public void OnMainCharacterDie(GameCharacterBehaviour characterBehaviour)
        {
            Debug.LogError("MainCharacter Die");

            characterBehaviour.OnDie -= OnMainCharacterDie;
            characterBehaviour.gameObject.SetActive(false);

            EventManager.Lose();

            SavePlayerData.SaveData();
        }
    }
}