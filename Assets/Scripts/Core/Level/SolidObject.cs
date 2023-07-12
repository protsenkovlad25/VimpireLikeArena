using UnityEngine;
using VampireLike.Core.General;

namespace VampireLike.Core.Levels
{
    public class SolidObject : MonoBehaviour
    {
        private void Start()
        {
            EventManager.OnAllWavesSpawned.AddListener(Destroy);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
