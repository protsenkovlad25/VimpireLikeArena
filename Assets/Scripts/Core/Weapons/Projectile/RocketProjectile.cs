using System.Collections;
using UnityEngine;
using VampireLike.Core.Movements;

namespace VampireLike.Core.Weapons
{
    public class RocketProjectile : Projectile
    {
        [SerializeField] private GameObject m_ExplosionPrefab;

        private Vector3 m_Target;
        private Explosion m_Explosion;

        public void Init()
        {
            ((RocketProjectileMovement)m_Moving).OnRocketMoveEnd += RocketMoveEnd;
        }

        void RocketMoveEnd()
        {
            StartCoroutine(Explosion());
            m_IsMove = false;
        }

        public override void Move(float speed, Vector3 point, float distance)
        {
            m_Target = point;

            base.Move(speed, point, distance);
        }

        protected override void Update()
        {
            if (!m_IsMove)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
                return;
            }

            m_FlyTime -= Time.deltaTime;
            var step = m_Speed * Time.deltaTime;
            var oldPostion = transform.position;

            m_Moving.Move(m_Target, step, transform, gameObject.GetComponent<Rigidbody>());

            if (m_FlyTime <= 0)
            {
                m_IsMove = false;
                gameObject.SetActive(false);
            }

        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                base.OnCollisionEnter(collision);
            }

            if (collision.gameObject.tag == "Wall")
            {
                RocketMoveEnd();
            }
        }

        private IEnumerator Explosion()
        {
            m_Explosion = Instantiate(m_ExplosionPrefab, transform).GetComponent<Explosion>();
            m_Explosion.Init(m_Target, transform, Damage);

            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);
        }
    }
}
