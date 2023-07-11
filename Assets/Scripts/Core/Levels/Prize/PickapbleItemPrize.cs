using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;

public class PickapbleItemPrize : PickapblePrize
{
    private GameObject m_ItemPrefab;
    private ItemObject m_ItemObject;

    public override void Initialize()
    {
        m_ItemObject = Instantiate(m_ItemPrefab, m_PrizePoint).GetComponent<ItemObject>();
        m_ItemObject.SetEnemeisController(m_ItemPrefab.GetComponent<ItemObject>().GetEnemeisController());
        m_ItemObject.SetWeaponsController(m_ItemPrefab.GetComponent<ItemObject>().GetWeaponsController());
        base.Initialize();
    }

    public override void GetPrize(MainCharacter mainCharacter)
    {
        m_ItemObject.UseItem(mainCharacter);
    }

    public void SetItemPrefab(GameObject itemprefab)
    {
        m_ItemPrefab = itemprefab;
    }
}
