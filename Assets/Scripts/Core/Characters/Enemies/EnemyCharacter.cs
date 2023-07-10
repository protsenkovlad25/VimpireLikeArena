using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters.Enemies
{
    public class EnemyCharacter : GameCharacterBehaviour, INeedingWeapon, INeeding<IAttaching>
    {
        [SerializeField] private List<Transform> m_WeaponPoints;
        [SerializeField] private List<WeaponVariant> m_WeaponVariants;
        [SerializeField] private EnemyType m_EnemyType;

        public EnemyType EnemyType { get; set; }

        private IAttaching m_Attaching;

        private bool m_IsMove;

        public List<WeaponVariant> GetWeaponVariants()
        {
            return m_WeaponVariants;
        }

        public List<Transform> GetWeaponPoints()
        {
            return m_WeaponPoints;
        }

        public EnemyType GetEnemyType()
        {
            return m_EnemyType;
        }

        public void SetWeaponVariant(WeaponVariant weaponVariant)
        {
        }

        public void Move(IAttaching targetPosition)
        {
            if (m_IsMove)
            {
                return;
            }

            StartCoroutine(MoveCoroutine(targetPosition));
        }

        public void InitWeapon()
        {
            foreach (var weapon in m_CharacterWeapons)
                weapon.Init();
        }

        public void Rotate(Vector3 angle)
        {
            transform.eulerAngles = angle;
        }

        public void Set(WeaponBehaviour generic)
        {
            generic.gameObject.layer = 9;

            if (m_CharacterWeapons == null)
            {
                m_CharacterWeapons = new List<CharacterWeapon>();
            }

            m_CharacterWeapons.Add(new CharacterWeapon());
            m_CharacterWeapons.Last().Set(m_Attaching);
            m_CharacterWeapons.Last().AddWeapon(generic, this);
            m_CharacterWeapons.Last().Init();
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public Transform Where()
        {
            return m_WeaponPoints[0];
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

        private IEnumerator MoveCoroutine(IAttaching targetPosition)
        {
            while (gameObject.activeInHierarchy)
            {
                m_IsMove = true;

                //if (m_Looking.LookShooting())
                //    StartShoot();
                //else
                //    StopShoot();

                Vector3 positionToMove = m_Looking.Look(targetPosition.GetTarget().position, transform, CharacterData.Speed);

                m_Moving.Move(positionToMove, 
                    CharacterData.Speed * Time.deltaTime, 
                    transform, 
                    gameObject.GetComponent<Rigidbody>());
                yield return null;
            }

            m_IsMove = false;

            yield break;
        }

        private IEnumerator PauseMove()
        {
            if (m_EnemyType != EnemyType.PushingEnemy)
            {
                m_Moving.Stop();
                yield return new WaitForSeconds(0.25f);
                m_Moving.Start();
            }
        }

        public override void TakeDamage(int damage)
        {
            //m_Moving.Stop();
            base.TakeDamage(damage);

            if (CurrentHealthPoint > 0)
            {
                StartCoroutine(PauseMove());
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<SolidObject>(out _))
            {
                m_Looking.ClearLook();
            }
        }
    }
}