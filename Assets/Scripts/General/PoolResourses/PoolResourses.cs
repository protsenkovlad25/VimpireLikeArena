using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.Levels;
using VampireLike.Core.Weapons;

namespace VampireLike.General
{
    public static class PoolResourses
    {
        private static List<GameObject> m_ItemObjects;
        private static List<GameObject> m_Weapons;

        public static void LoadItems(EnemeisController enemeisController, WeaponsController weaponsController)
        {
            m_ItemObjects = new List<GameObject>();
            Object[] objects = Resources.LoadAll("Items", typeof(GameObject));

            for (int i = 0; i < objects.Length; i++)
            {
                m_ItemObjects.Add((GameObject)objects[i]);
                m_ItemObjects[i].GetComponent<ItemObject>().SetEnemeisController(enemeisController);
                m_ItemObjects[i].GetComponent<ItemObject>().SetWeaponsController(weaponsController);
            }
        }

        public static void LoadWeapons()
        {
            m_Weapons = new List<GameObject>();
            Object[] objects = Resources.LoadAll("WeaponsForPrize", typeof(GameObject));

            foreach (Object obj in objects)
            {
                m_Weapons.Add((GameObject)obj);
            }
        }

        public static List<GameObject> GetItemObjects()
        {
            return m_ItemObjects;
        }

        public static List<GameObject> GetWeaponObjects()
        {
            return m_Weapons;
        }
    }
}
