using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VampireLike.Core.Characters;
using VampireLike.Core.Characters.Enemies;
using VampireLike.Core.General;
using VampireLike.Core.Weapons;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Image m_ItemImage;

    protected EnemeisController m_EnemeisController;
    protected WeaponsController m_WeaponsController;

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
