using System.Threading.Tasks;
using UnityEngine;

namespace VampireLike.Core.Characters
{
    public class DashMovement : IMoving
    {
        private bool m_IsStop;
        private bool m_IsDash;
        private bool m_IsCharging;
        private float m_Force = 60f;

        public void Move(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            if (m_IsDash)
            {
                return;
            }
            else if (m_IsCharging)
            {
                var direction = (target - transform.position).normalized;

                var lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed);
            }
            else Charging(transform, rigidbody);
        }

        private async Task Charging(Transform transform, Rigidbody rigidbody)
        {
            m_IsCharging = true;
            await Task.Delay(System.TimeSpan.FromSeconds(3f));

            await Dash(transform, rigidbody);


            await Task.Delay(System.TimeSpan.FromSeconds(.25f));


            await DashStop(rigidbody);

            m_IsDash = false;
            m_IsCharging = false;
            transform.GetComponentInChildren<ParticleSystem>().Stop();
        }

        private async Task Dash(Transform transform, Rigidbody rigidbody)
        {
            m_IsDash = true;
            m_IsCharging = false;

            Vector3 force = transform.forward * m_Force;


            rigidbody.AddForce(force, ForceMode.Impulse);

            transform.GetComponentInChildren<ParticleSystem>().Play();
        }

        private async Task DashStop(Rigidbody rigidbody)
        {
            rigidbody.AddForce(-rigidbody.velocity*.9f, ForceMode.Impulse);
        }

        public void Start()
        {
            m_IsStop = false;
        }

        public void Stop()
        {
            m_IsStop = true;
        }
    }
}
