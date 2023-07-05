using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

public class Prize
{
    public List<GameObject> ArenaPrizes { get; set; }

    public Prize(GameObject prefab, int count = 1)
    {
        ArenaPrizes = new List<GameObject>();

        for(int i = 0; i < count; i++)
        {
            ArenaPrizes.Add(Object.Instantiate(prefab));
            ArenaPrizes[i].GetComponent<PickapbleWeaponPrize>().WeaponType = WeaponType.TestWeapon;
            ArenaPrizes[i].GetComponent<PickapbleWeaponPrize>().Initialize();
            ArenaPrizes[i].SetActive(false);
        }
    }

    public void SpawnPrizes(Vector3 spawnPosition)
    {
        Vector3 position = spawnPosition;
        float dif = 1;

        foreach (var prize in ArenaPrizes)
        {
            prize.GetComponent<PickapbleWeaponPrize>().transform.position = position;
            prize.SetActive(true);

            position.x = dif * 10f * (-1f);
            dif = -(dif + 1);
        }
    }
}
