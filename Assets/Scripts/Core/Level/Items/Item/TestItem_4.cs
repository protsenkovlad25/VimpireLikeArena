using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Levels
{
    public class TestItem_4 : ItemObject
    {
        private int m_AddHealthBonus = 10;

        public override void UseItem(GameCharacterBehaviour character)
        {
            if (character.CurrentHealthPoint != character.StartedHealthPoint)
                character.CharacterData.HealthPoints += m_AddHealthBonus;
        }
    }
}
