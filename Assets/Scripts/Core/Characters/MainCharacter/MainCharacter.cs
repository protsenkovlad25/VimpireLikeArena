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
        [SerializeField] private List<Transform> m_WeaponPoints;
        [SerializeField] private WeaponVariant m_MainWeaponVariant;
        [SerializeField] private Transform m_MainWeaponPoint;

        [SerializeField] private float m_SafeTime;
        [SerializeField] private float m_FadeTime;
        [SerializeField] private bool m_TakeDamage;

        private List<WeaponVariant> m_WeaponsOnMainCharacter;
        private IAttaching m_Attaching;

        public WeaponVariant MainWeaponVariant => m_MainWeaponVariant;
        public List<WeaponVariant> WeaponsOnMainCharacter => m_WeaponsOnMainCharacter;

        public void Start()
        {
            m_WeaponsOnMainCharacter = new List<WeaponVariant>();

            for (int i = 0; i < m_WeaponPoints.Count; i++)
                m_WeaponsOnMainCharacter.Add(WeaponVariant.None);
        }

        public void Move(Vector2 deriction)
        {
            m_Moving.Move(new Vector3(deriction.x, 0f, deriction.y), m_CharacterData.Speed, transform, gameObject.GetComponent<Rigidbody>());
        }

        public List<WeaponVariant> GetWeaponVariants()
        {
            return new List<WeaponVariant> { m_MainWeaponVariant };
        }

        public List<Transform> GetWeaponPoints()
        {
            return new List<Transform> { m_MainWeaponPoint };
        }

        public void SetWeaponVariant(WeaponVariant weaponVariant)
        {
            for (int i = 0; i < m_WeaponsOnMainCharacter.Count; i++)
                if (m_WeaponsOnMainCharacter[i] == WeaponVariant.None)
                {
                    m_WeaponsOnMainCharacter[i] = weaponVariant;
                    break;
                }
        }

        public Transform Where()
        {
            if (m_WeaponsOnMainCharacter != null)
            {
                for (int i = 0; i < m_WeaponsOnMainCharacter.Count; i++)
                    if (m_WeaponsOnMainCharacter[i] == WeaponVariant.None)
                        return m_WeaponPoints[i];
            }

            return m_MainWeaponPoint;
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

        public void InitWeapon()
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