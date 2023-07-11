using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;

public class TestItem : ItemObject
{
    private int m_ItemDamage = 10;
    private float m_ItemFireRate = 0.1f;
    private int m_ItemHealth = 100;
    private float m_ItemEnemySpeed = -5;

    private WeaponType m_ItemWeaponType = WeaponType.Directed;
    private WeaponClass m_ItemWeaponClass = WeaponClass.Plasma;

    public override void UseItem(GameCharacterBehaviour gameCharacterBehaviour)
    {
        base.UseItem(gameCharacterBehaviour);


        // -- Weapon Data Changes -- //

        /*foreach (var weapon in gameCharacterBehaviour.CharacterWeapons)
        {
            foreach (var weap in weapon.Weapons)
            {
                Debug.Log("BEFORE: " + weap.GetWeaponData().Damage + " - " + weap.GetWeaponData().FireRate);

                weap.GetWeaponData().Damage += m_ItemDamage;
                weap.GetWeaponData().FireRate = m_ItemFireRate;

                Debug.Log("AFTER: " + weap.GetWeaponData().Damage + " - " + weap.GetWeaponData().FireRate);
                Debug.Log("-----------------");
            }
        }*/


        // -- Character Data Changes -- //

        /*Debug.Log("BEFORE: " + gameCharacterBehaviour.CharacterData.HealthPoints);
        gameCharacterBehaviour.CharacterData.HealthPoints += m_ItemHealth;
        Debug.Log("AFTER: " + gameCharacterBehaviour.CharacterData.HealthPoints);*/


        // -- Enemies Data Changes -- //

        /*Debug.Log("BEFORE: " + m_EnemeisController.SpeedModificator);
        m_EnemeisController.SpeedModificator += m_ItemEnemySpeed;
        Debug.Log("AFTER: " + m_EnemeisController.SpeedModificator);*/


        // -- Changes Data based on Type or Class -- //
        foreach (var weaponTypeDataHolder in m_WeaponsController.WeaponTypeDataHolders)
        {
            if (weaponTypeDataHolder.WeaponType == m_ItemWeaponType)
            {
                weaponTypeDataHolder.Damage += m_ItemDamage;
            }
        }

        //foreach (var weaponClassDataHolder in m_WeaponsController.WeaponClassDataHolders)
        //{
        //    if (weaponClassDataHolder.WeaponClass == m_ItemWeaponClass)
        //    {
        //        weaponClassDataHolder.FireRate = m_ItemFireRate;
        //    }
        //}
    }
}
