using UnityEngine;

namespace VampireLike.Core
{
    public interface IBypassing
    {
        void Bypass(float speed, Transform transform, Vector3 difference);
    }
}
