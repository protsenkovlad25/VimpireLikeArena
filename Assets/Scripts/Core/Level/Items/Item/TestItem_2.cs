using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Levels
{
    public class TestItem_2 : ItemObject
    {
        private int m_DamageBonus = 1;

        public override void UseItem(GameCharacterBehaviour character)
        {
            character.CharacterData.BaseDamage += m_DamageBonus;
        }
    }
}
