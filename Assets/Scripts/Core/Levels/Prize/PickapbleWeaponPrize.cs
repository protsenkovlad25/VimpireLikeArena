using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

public class PickapbleWeaponPrize : PickapblePrize, INeedingWeapon
{

    private WeaponType m_WeaponType;

    public WeaponType WeaponType
    {
        get => m_WeaponType;
        set => m_WeaponType = value;
    }

    public override void Initialize()
    {
        EventManager.WeaponReceived(m_WeaponType, this);
        base.Initialize();
    }

    public override void GetPrize(MainCharacter mainCharacter)
    {
        EventManager.WeaponReceived(m_WeaponType, mainCharacter);
        Destroy(gameObject);
    }

    public Transform Where()
    {
        return m_PrizePoint;
    }

    public List<WeaponType> GetWeaponTypes()
    {
        return new List<WeaponType> { m_WeaponType };
    }

    public List<Transform> GetWeaponPoints()
    {
        return new List<Transform> { m_PrizePoint };
    }

    public void SetWeaponType(WeaponType weaponType)
    { }

    public void Set(WeaponBehaviour generic)
    { }
}
