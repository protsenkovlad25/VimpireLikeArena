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

        public void Move(Vector2 deriction)
        {
            m_Moving.Move(new Vector3(deriction.x, 0f, deriction.y), m_CharacterData.Speed, transform, gameObject.GetComponent<Rigidbody>());
        }

        public List<Transform> GetWeaponPoints()
        {
            return m_WeaponPoints;
        }

        public List<GameObject> GetWeaponPrefabs()
        {
            return m_WeaponPrefabs;
        }

        public Transform Where()
        {
            for (int i = 0; i < m_WeaponPoints.Count; i++)
                if (m_WeaponObjects[i] == null)
                    return m_WeaponPoints[i];

            return null;
        }

        public void Set(GameObject weapon)
        {
            weapon.layer = 7;

            for (int i = 0; i < m_WeaponObjects.Count; i++)
            {
                if (m_WeaponObjects[i] == null)
                {
                    m_WeaponObjects[i] = weapon;
                    break;
                }
            }

            m_CharacterWeapon.AddWeapon(weapon.GetComponent<WeaponBehaviour>());
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