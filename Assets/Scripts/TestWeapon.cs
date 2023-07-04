using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

public class TestWeapon : WeaponBehaviour, INeeding<IAttaching>
{
    private WeaponType weaponType = WeaponType.TestWeapon;
    private IAttaching m_Attaching;

    public WeaponType WeaponType => weaponType;

    public override void Init()
    {
    }

    public void Set(IAttaching generic)
    {
        m_Attaching = generic;
    }

    public override void Shoot()
    {
    }

    public override void Stop()
    {
    }
}
