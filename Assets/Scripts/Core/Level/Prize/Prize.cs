using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;
using VampireLike.General;

namespace VampireLike.Core.Levels
{
    public class Prize
    {
        public List<GameObject> ArenaPrizes { get; set; }

        public Prize(GameObject prefab, int count = 1)
        {
            ArenaPrizes = new List<GameObject>();

            // -- Wepoan -- //
            /*for(int i = 0; i < count; i++)
            {
                ArenaPrizes.Add(Object.Instantiate(prefab));
                ArenaPrizes[i].GetComponent<PickapbleWeaponPrize>().WeaponVariant = WeaponVariant.SimpleShooting;
                ArenaPrizes[i].GetComponent<PickapblePrize>().Initialize();
                ArenaPrizes[i].GetComponent<PickapblePrize>().OnGet = DestroyObjects;
                ArenaPrizes[i].SetActive(false);
            }*/


            // -- Item -- //
            for (int i = 0; i < count; i++)
            {
                ArenaPrizes.Add(Object.Instantiate(prefab));
                ArenaPrizes[i].GetComponent<PickapbleItemPrize>().SetItemPrefab(PoolResourses.GetItemObjects()[0]);
                ArenaPrizes[i].GetComponent<PickapblePrize>().Initialize();
                ArenaPrizes[i].GetComponent<PickapblePrize>().OnGet = DestroyObjects;
                ArenaPrizes[i].SetActive(false);
            }
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
