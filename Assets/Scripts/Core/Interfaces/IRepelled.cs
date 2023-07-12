using UnityEngine;

namespace VampireLike.Core
{
    public interface IRepelled
    {
        void Push(Vector3 direction, float force, ForceMode mode = ForceMode.Force);
    }
}