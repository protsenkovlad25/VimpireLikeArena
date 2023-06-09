using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.General;
using VampireLike.Core.Weapons;
using VampireLike.General;

namespace VampireLike.Core.Levels
{
    public class PickapbleWeaponPrize : PickapblePrize, INeedingWeapon
    {
        private WeaponVariant m_WeaponVariant;
        private WeaponBehaviour m_Weapon;
        private ItemObject m_ItemObject;

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
            foreach (var weapon in mainCharacter.CharacterWeapon.Weapons)
            {
                if (weapon.WeaponType == m_Weapon.WeaponType)
                {
                    // TypeSynergy
                    //m_ItemObject = PoolResourses.GetItemObjects().Equals(item => item.ItemType == ItemType.SynergyType);
                    foreach (var item in PoolResourses.GetItemObjects())
                    {
                        if (item.GetComponent<ItemObject>().ItemType == ItemType.SynergyType)
                        {
                            m_ItemObject = item.GetComponent<ItemObject>();
                            break;
                        }
                    }

                    // применить полученный item

                    break;
                }
            }

            foreach (var weapon in mainCharacter.CharacterWeapon.Weapons)
            {
                if (weapon.WeaponClass == m_Weapon.WeaponClass)
                {
                    // ClassSynergy
                    break;
                }
            }

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
        {
            m_Weapon = generic;
        }
    }
}
