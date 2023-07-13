using UnityEngine;
using VampireLike.Core.Characters;

namespace VampireLike.Core.Levels
{
    public class PickapbleItemPrize : PickapblePrize
    {
        private GameObject m_ItemPrefab;
        private ItemObject m_ItemObject;

        public override void Initialize()
        {
            m_ItemObject = Instantiate(m_ItemPrefab, m_PrizePoint).GetComponent<ItemObject>();
            m_ItemObject.SetEnemeisController(m_ItemPrefab.GetComponent<ItemObject>().GetEnemeisController());
            m_ItemObject.SetWeaponsController(m_ItemPrefab.GetComponent<ItemObject>().GetWeaponsController());

            m_ItemObject.Init();

            base.Initialize();
        }

        public override void GetPrize(MainCharacter mainCharacter)
        {
            m_ItemObject.UseItem(mainCharacter);
        }

        public void SetItemPrefab(GameObject itemPrefab)
        {
            m_ItemPrefab = itemPrefab;
        }
    }
}
