using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VampireLike.Core.Levels;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters
{
    public class EnemyCharacter : GameCharacterBehaviour, INeedingWeapon, INeeding<IAttaching>
    {
        [SerializeField] private EnemyType m_EnemyType;

        public EnemyType EnemyType { get; set; }

        private bool m_IsMove;

        public List<GameObject> GetWeaponPrefabs()
        {
            return m_WeaponPrefabs;
        }

        public List<Transform> GetWeaponPoints()
        {
            return m_WeaponPoints;
        }

        public EnemyType GetEnemyType()
        {
            return m_EnemyType;
        }

        public void Move(IAttaching targetPosition)
        {
            if (m_IsMove)
            {
                return;
            }

            StartCoroutine(MoveCoroutine(targetPosition));
        }

        public void InitWeapons()
        {
            m_CharacterWeapon.Init();
        }

        public void Rotate(Vector3 angle)
        {
            transform.eulerAngles = angle;
        }

        public void Set(GameObject weapon)
        {
            weapon.layer = 9;

            for (int i = 0; i < m_WeaponObjects.Count; i++)
            {
                if (m_WeaponObjects[i] == null)
                {
                    m_WeaponObjects[i] = weapon;
                    break;
                }
            }

            m_CharacterWeapon.AddWeapon(weapon.GetComponent<WeaponBehaviour>(), this);
            m_CharacterWeapon.Weapons.Last().Init();
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public Transform Where()
        {
            for (int i = 0; i < m_WeaponPoints.Count; i++)
                if (m_WeaponObjects[i] == null)
                    return m_WeaponPoints[i];

            return null;
        }

        public void StartShoot()
        {
            m_CharacterWeapon.StartShoot();
        }

        public void StopShoot()
        {
            m_CharacterWeapon.Stop();
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

                Vector3 positionToMove = m_Looking.Look(targetPosition.GetTarget().position, transform, m_CharacterData.Speed);

                m_Moving.Move(positionToMove, 
                    m_CharacterData.Speed * Time.deltaTime, 
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