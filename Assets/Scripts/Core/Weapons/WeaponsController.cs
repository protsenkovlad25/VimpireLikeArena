using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public class WeaponsController : MonoBehaviour
    {
        [SerializeField] private WeaponConfigurator m_WeaponConfigurator;

        public void GaveWeapons(IEnumerable<INeedingWeapon> needingWeapons)
        {
            foreach (var item in needingWeapons)
            {
                GaveWeapon(item);
            }
        }

        public void GaveWeapon(INeedingWeapon needingWeapon)
        {
            if (needingWeapon.GetWeaponType().Equals(WeaponType.None))
            {
                return;
            }

            var data = m_WeaponConfigurator.GetData(needingWeapon.GetWeaponType());

            var builder = new WeaponBuilder(needingWeapon.Where())
                                .SetWeaponData(data.WeaponData)
                                .SetWeaponBehaviour(data.WeaponBehaviour);

            needingWeapon.Set(builder.Build());
        }


    }
}