using System.Collections;
using UnityEngine;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Characters.Enemies
{
    public class EnemyCharacter : GameCharacterBehaviour, INeedingWeapon, INeeding<IAttaching>
    {
        [SerializeField] private Transform m_WeaponPoint;
        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private EnemyType m_EnemyType;

        public WeaponType WeaponType { get; set; }
        public EnemyType EnemyType { get; set; }

        private CharacterWeapon m_CharacterWeapon;
        private IAttaching m_Attaching;

        private bool m_IsMove;

        public WeaponType GetWeaponType()
        {
            return m_WeaponType;
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

        public void InitWeapon()
        {
            m_CharacterWeapon.Init();
        }

        public void Rotate(Vector3 angle)
        {
            transform.eulerAngles = angle;
        }

        public void Set(WeaponBehaviour generic)
        {
            generic.gameObject.layer = 9;//TODO
            if (m_CharacterWeapon == null)
            {
                m_CharacterWeapon = new CharacterWeapon();
                m_CharacterWeapon.Set(m_Attaching);
            }

            m_CharacterWeapon.AddWeapon(generic, this);
        }

        public void Set(IAttaching generic)
        {
            m_Attaching = generic;
        }

        public Transform Where()
        {
            return m_WeaponPoint;
        }

        public void StartShoot()
        {
            m_CharacterWeapon.Start();
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

                if (m_Looking.LookShooting())
                    StartShoot();
                else
                    StopShoot();

                Vector3 positionToMove = m_Looking.Look(targetPosition.GetTarget().position, transform);

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