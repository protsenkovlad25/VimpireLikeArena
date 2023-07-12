using UnityEngine;
using UnityEngine.UI;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.Levels
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private Image m_ItemImage;

        protected EnemeisController m_EnemeisController;
        protected WeaponsController m_WeaponsController;

        protected ItemType m_ItemType = ItemType.None;

        public ItemType ItemType => m_ItemType;

        public virtual void Init()
        { }

        public virtual void UseItem(GameCharacterBehaviour gameCharacterBehaviour)
        {

        }

        public virtual void SetEnemeisController(EnemeisController enemeisController)
        {
            m_EnemeisController = enemeisController;
        }

        public virtual void SetWeaponsController(WeaponsController weaponsController)
        {
            m_WeaponsController = weaponsController;
        }

        public virtual EnemeisController GetEnemeisController()
        {
            return m_EnemeisController;
        }

        public virtual WeaponsController GetWeaponsController()
        {
            return m_WeaponsController;
        }
    }

    public enum ItemType
    {
        None,
        SynergyType,
        SynergyClass
    }
}
