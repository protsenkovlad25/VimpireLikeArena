using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Levels
{
    public class TestItem_3 : ItemObject
    {
        private float m_SpeedBonus = 1.1f;

        public override void UseItem(GameCharacterBehaviour character)
        {
            character.CharacterData.Speed *= m_SpeedBonus;
        }
    }
}
