using VampireLike.Core.Characters;

namespace VampireLike.Core.Movements
{
    [System.Serializable]
    public class DashMovementData : CharacterData
    {
        public float DashForce { get; set; }
    }
}
