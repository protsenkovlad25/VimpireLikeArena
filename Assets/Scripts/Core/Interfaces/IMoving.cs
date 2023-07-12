using UnityEngine;

namespace VampireLike.Core
{
    public interface IMoving
    {
        void Move(Vector3 direction, float speed, Transform transform, Rigidbody rigidbody);

        void Stop();

        void Start();
    }
}