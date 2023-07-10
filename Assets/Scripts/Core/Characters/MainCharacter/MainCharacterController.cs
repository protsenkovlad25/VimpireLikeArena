using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VampireLike.Core.Movements;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters
{
    public class MainCharacterController : MonoBehaviour, IAttaching, IIniting
    {
        [SerializeField] private MainCharacter m_MainCharacter;

        public INeedingWeapon NeedingWeapon => m_MainCharacter.GetComponent<INeedingWeapon>();

        public void StartShoot()
        {
            m_MainCharacter.StartShoot();
        }

        public void StopShoot()
        {
            m_MainCharacter.StopShoot();
        }

        public void SetAttaching(IAttaching attaching)
        {
            if (attaching == null)
            {
                Debug.LogError($"Class - {nameof(MainCharacterController)}:\n" +
                               $"Method - {nameof(SetAttaching)}. Null References - {nameof(attaching)}.");
                return;
            }

            m_MainCharacter.Set(attaching);
        }

        public Transform GetTarget()
        {
            return m_MainCharacter.transform;
        }

        public void Init()
        {
            m_MainCharacter.SetCharacterMovement(new ControlledMovement());
            m_MainCharacter.SetCharacterData(new CharacterData()
            {
                Speed = 8,
                HealthPoints = 30,
                BaseDamage = 1
            });
            m_MainCharacter.Init();
            m_MainCharacter.InitWeapon();
            m_MainCharacter.OnDie += OnMainCharacterDie;
        }

        public void Move(Vector2 vector2)
        {
            m_MainCharacter.Move(vector2);
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