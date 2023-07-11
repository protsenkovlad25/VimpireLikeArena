using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

public class PickapbleWeaponPrize : PickapblePrize, INeedingWeapon
{
    private WeaponVariant m_WeaponVariant;

    public WeaponVariant WeaponVariant
    {
        get => m_WeaponVariant;
        set => m_WeaponVariant = value;
    }

    public override void Initialize()
    {
        EventManager.WeaponReceived(m_WeaponVariant, this);
        base.Initialize();
    }

    public override void GetPrize(MainCharacter mainCharacter)
    {
        EventManager.WeaponReceived(m_WeaponVariant, mainCharacter);
        Destroy(gameObject);
    }

    public Transform Where()
    {
        return m_PrizePoint;
    }

    public List<WeaponVariant> GetWeaponVariants()
    {
        return new List<WeaponVariant> { m_WeaponVariant };
    }

    public List<Transform> GetWeaponPoints()
    {
        return new List<Transform> { m_PrizePoint };
    }

    public void SetWeaponVariant(WeaponVariant weaponVariant)
    { }

    public void Set(WeaponBehaviour generic)
    { }
}
