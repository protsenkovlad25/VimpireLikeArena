using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Input;
using DG.Tweening;
using VampireLike.Core.Weapons;
using System.Linq;

namespace VampireLike.Core.Characters
{
    public class MainCharacter : GameCharacterBehaviour, IHero, INeedingWeapon
    {
        [SerializeField] private List<Transform> m_WeaponPoints;
        [SerializeField] private WeaponType m_MainWeaponType;
        [SerializeField] private Transform m_MainWeaponPoint;

        [SerializeField] private float m_SafeTime;
        [SerializeField] private float m_FadeTime;
        [SerializeField] private bool m_TakeDamage;

        private List<WeaponType> m_WeaponsOnMainCharacter;
        private IAttaching m_Attaching;

        public List<Transform> WeaponPoints => m_WeaponPoints;

        public void Start()
        {
            m_WeaponsOnMainCharacter = new List<WeaponType>();

            for (int i = 0; i < WeaponPoints.Count; i++)
                m_WeaponsOnMainCharacter.Add(WeaponType.None);
        }

        public void Move(Vector2 deriction)
        {
            m_Moving.Move(new Vector3(deriction.x, 0f, deriction.y), CharacterData.Speed, transform, gameObject.GetComponent<Rigidbody>());
        }

        public List<WeaponType> GetWeaponTypes()
        {
            return new List<WeaponType> { m_MainWeaponType };
        }

        public List<Transform> GetWeaponPoints()
        {
            return new List<Transform> { m_MainWeaponPoint };
        }

        public void SetWeaponType(WeaponType weaponType)
        {
            for (int i = 0; i < m_WeaponsOnMainCharacter.Count; i++)
                if (m_WeaponsOnMainCharacter[i] == WeaponType.None)
                {
                    m_WeaponsOnMainCharacter[i] = weaponType;
                    break;
                }
        }

        public Transform Where()
        {
            if (m_WeaponsOnMainCharacter != null)
            {
                for (int i = 0; i < m_WeaponsOnMainCharacter.Count; i++)
                    if (m_WeaponsOnMainCharacter[i] == WeaponType.None)
                        return WeaponPoints[i];
            }

            return m_MainWeaponPoint;
        }

        public void Set(WeaponBehaviour generic)
        {
            generic.gameObject.layer = 7;

            if (m_CharacterWeapons == null)
            {
                m_CharacterWeapons = new List<CharacterWeapon>();
            }

            m_CharacterWeapons.Add(new CharacterWeapon());
            m_CharacterWeapons.Last().Set(m_Attaching);
            m_CharacterWeapons.Last().AddWeapon(generic);
            m_CharacterWeapons.Last().Init();
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public void InitWeapon()
        {
            foreach (var weapon in m_CharacterWeapons)
                weapon.Init();
        }

        public void StartShoot()
        {
            foreach (var weapon in m_CharacterWeapons)
                weapon.StartShoot();
        }

        public void StopShoot()
        {
            foreach (var weapon in m_CharacterWeapons)
                weapon.Stop();
        }

        public override void TakeDamage(int damage)
        {
            if (!m_TakeDamage)
            {
                return;
            }
            m_TakeDamage = false;

            base.TakeDamage(damage);

            //TakingDamage?.Invoke();
            EventManager.TakingDamage();

            Debug.Log("I take Damage");

            StartCoroutine(TakeDamageCoroutine());
            StartCoroutine(TransparencyChange());
        }

        private IEnumerator TransparencyChange()
        {
            while (!m_TakeDamage)
            {
                yield return StartCoroutine(Fade());
                yield return StartCoroutine(Rise());
            }
        }

        private IEnumerator Fade()
        {
            gameObject.GetComponent<MeshRenderer>().material.DOFade(0.5f, m_FadeTime);

            yield return new WaitForSeconds(m_FadeTime);
        }

        private IEnumerator Rise()
        {
            gameObject.GetComponent<MeshRenderer>().material.DOFade(1, m_FadeTime);

            yield return new WaitForSeconds(m_FadeTime);
        }

        private IEnumerator TakeDamageCoroutine()
        {
            yield return new WaitForSeconds(m_SafeTime);
            m_TakeDamage = true;
        }
    }
}