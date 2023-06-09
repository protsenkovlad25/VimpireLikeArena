using System.Threading.Tasks;
using UnityEngine;

namespace VampireLike.Core.Movements
{
    public class DashMovement : IMoving
    {
        private bool m_IsStop;
        private bool m_IsDash;
        private bool m_IsCharging;
        private float m_Force = 750;

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
            else Charging(target, speed, transform, rigidbody);
        }

        private async Task Charging(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            m_IsCharging = true;
            await Task.Delay(System.TimeSpan.FromSeconds(3f));
            
            await Dash(target, speed, transform, rigidbody);

            m_IsDash = false;
            m_IsCharging = false;
        }

        private async Task Dash(Vector3 target, float speed, Transform transform, Rigidbody rigidbody)
        {
            m_IsDash = true;
            m_IsCharging = false;

            Vector3 force = transform.forward * m_Force;

            rigidbody.AddForce(force, ForceMode.Force);
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
