using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VampireLike.Core.Weapons;
using System.Linq;
using VampireLike.Core.General;

namespace VampireLike.Core.Characters
{
    public class MainCharacter : GameCharacterBehaviour, IHero, INeedingWeapon
    {
        [SerializeField] private float m_SafeTime;
        [SerializeField] private float m_FadeTime;
        [SerializeField] private bool m_TakeDamage;

        public void Start()
        {
            //m_WeaponsOnMainCharacter = new List<WeaponVariant>();

            //for (int i = 0; i < m_WeaponPoints.Count; i++)
            //    m_WeaponsOnMainCharacter.Add(WeaponVariant.None);
        }

        public override void Init()
        {
            base.Init();

            GameObject weapon;
            for (int i = 0; i < m_WeaponPrefabs.Count; i++)
            {
                weapon = Instantiate(m_WeaponPrefabs[i], m_WeaponPoints[i]);
                weapon.layer = 7;

                m_WeaponObjects.Add(weapon);
                m_CharacterWeapon.AddWeapon(m_WeaponObjects[i].GetComponent<WeaponBehaviour>());
            }
        }

        public void Move(Vector2 deriction)
        {
            m_Moving.Move(new Vector3(deriction.x, 0f, deriction.y), m_CharacterData.Speed, transform, gameObject.GetComponent<Rigidbody>());
        }

        public List<WeaponVariant> GetWeaponVariants()
        {
            //return new List<WeaponVariant> { m_MainWeaponVariant };
            return null;
        }

        public List<Transform> GetWeaponPoints()
        {
            //return new List<Transform> { m_MainWeaponPoint };
            return null;
        }

        public void SetWeaponVariant(WeaponVariant weaponVariant)
        {
            //for (int i = 0; i < m_WeaponsOnMainCharacter.Count; i++)
            //    if (m_WeaponsOnMainCharacter[i] == WeaponVariant.None)
            //    {
            //        m_WeaponsOnMainCharacter[i] = weaponVariant;
            //        break;
            //    }
        }

        public Transform Where()
        {
            //if (m_WeaponsOnMainCharacter != null)
            //{
            //    for (int i = 0; i < m_WeaponsOnMainCharacter.Count; i++)
            //        if (m_WeaponsOnMainCharacter[i] == WeaponVariant.None)
            //            return m_WeaponPoints[i];
            //}

            return m_WeaponPoints[0];
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
            m_CharacterWeapon.Weapons.Last().Init();
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public void InitWeapons()
        {
            m_CharacterWeapon.Init();
        }

        public void StartShoot()
        {
            m_CharacterWeapon.StartShoot();
        }

        public void StopShoot()
        {
            m_CharacterWeapon.Stop();
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