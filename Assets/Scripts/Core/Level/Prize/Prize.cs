using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;
using VampireLike.General;

namespace VampireLike.Core.Levels
{
    public class Prize
    {
        private GameObject m_Prefab;
        private int m_Count;

        public List<GameObject> ArenaPrizes { get; set; }

        System.Random random;

        public Prize(GameObject prefab, int count = 1)
        {
            ArenaPrizes = new List<GameObject>();
            random = new System.Random();
            m_Prefab = prefab;
            m_Count = count;

            // -- Weapon -- //
            /*for(int i = 0; i < count; i++)
            {
                ArenaPrizes.Add(Object.Instantiate(prefab));
                ArenaPrizes[i].GetComponent<PickapbleWeaponPrize>().SetWeaponPrefab(PoolResourses.GetWeaponObjects()[0]);
                ArenaPrizes[i].GetComponent<PickapblePrize>().Initialize();
                ArenaPrizes[i].GetComponent<PickapblePrize>().OnGet = DestroyObjects;
                ArenaPrizes[i].SetActive(false);
            }*/


            // -- Item -- //
            /*for (int i = 0; i < count; i++)
            {
                ArenaPrizes.Add(Object.Instantiate(prefab));
                ArenaPrizes[i].GetComponent<PickapbleItemPrize>().SetItemPrefab(PoolResourses.GetItemObjects()[0]);
                ArenaPrizes[i].GetComponent<PickapblePrize>().Initialize();
                ArenaPrizes[i].GetComponent<PickapblePrize>().OnGet = DestroyObjects;
                ArenaPrizes[i].SetActive(false);
            }*/
        }

        public Prize InitializeWeaponPrizes()
        {
            List<GameObject> weaponsInPool = PoolResourses.GetWeaponObjects();

            for (int i = 0; i < m_Count; i++)
            {
                ArenaPrizes.Add(Object.Instantiate(m_Prefab));
                ArenaPrizes[i].GetComponent<PickapbleWeaponPrize>().SetWeaponPrefab(weaponsInPool[random.Next(0, weaponsInPool.Count)]);
                ArenaPrizes[i].GetComponent<PickapblePrize>().Initialize();
                ArenaPrizes[i].GetComponent<PickapblePrize>().OnGet = DestroyObjects;
                ArenaPrizes[i].SetActive(false);
            }

            return this;
        }

        public Prize InitializeItemPrizes()
        {
            List<GameObject> itemsInPool = PoolResourses.GetItemObjects();

            for (int i = 0; i < m_Count; i++)
            {
                ArenaPrizes.Add(Object.Instantiate(m_Prefab));
                ArenaPrizes[i].GetComponent<PickapbleItemPrize>().SetItemPrefab(itemsInPool[random.Next(0, itemsInPool.Count)]);
                ArenaPrizes[i].GetComponent<PickapblePrize>().Initialize();
                ArenaPrizes[i].GetComponent<PickapblePrize>().OnGet = DestroyObjects;
                ArenaPrizes[i].SetActive(false);
            }

            return this;
        }

        public void SpawnPrizes(Vector3 spawnPosition)
        {
            Vector3 position = spawnPosition;
            float i = 0;

            foreach (var prize in ArenaPrizes)
            {
                prize.transform.position = position;
                prize.SetActive(true);

                i++;
                if (i % 2 == 0)
                    position.x += 5 * -i;
                else
                    position.x += 5 * i;
            }
        }

        public void DestroyObjects()
        {
            foreach (var prize in ArenaPrizes)
                Object.Destroy(prize);
        }
    }
}
