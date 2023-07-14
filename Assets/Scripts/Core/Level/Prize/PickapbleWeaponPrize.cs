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
        private GameObject m_WeaponPrefab;
        private WeaponBehaviour m_WeaponObject;
        private ItemObject m_ItemObject;

        public override void Initialize()
        {
            EventManager.WeaponReceived(this, m_WeaponPrefab);

            base.Initialize();
        }

        public void SetWeaponPrefab(GameObject weaponPrefab)
        {
            m_WeaponPrefab = weaponPrefab;
        }

        public override void GetPrize(MainCharacter mainCharacter)
        {
            // --- —инерги€ - пока нету (доделать) --- //

            /*foreach (var weapon in mainCharacter.CharacterWeapon.Weapons)
            {
                if (weapon.WeaponType == m_WeaponObject.WeaponType)
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
                if (weapon.WeaponClass == m_WeaponObject.WeaponClass)
                {
                    // ClassSynergy
                    break;
                }
            }*/

            EventManager.WeaponReceived(mainCharacter, m_WeaponPrefab);
            Destroy(gameObject);
        }

        public Transform Where()
        {
            return m_PrizePoint;
        }

        public List<GameObject> GetWeaponPrefabs()
        {
            return null;
        }

        public List<Transform> GetWeaponPoints()
        {
            return new List<Transform> { m_PrizePoint };
        }

        public void Set(GameObject generic)
        {
        }
    }
}
