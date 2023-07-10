using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public class WeaponsController : MonoBehaviour
    {
        [SerializeField] private WeaponConfigurator m_WeaponConfigurator;

        public void Init()
        {
            EventManager.OnWeaponReceived.AddListener(GaveWeapon);
        }

        public void GaveWeapons(IEnumerable<INeedingWeapon> needingWeapons)
        {
            foreach (var item in needingWeapons)
            {
                GaveWeapon(item);
            } 
        }

        public void GaveWeapon(INeedingWeapon needingWeapon)
        {
            List<WeaponVariant> weaponVariants = needingWeapon.GetWeaponVariants();
            List<Transform> weaponPoints = needingWeapon.GetWeaponPoints();

            for (int i = 0; i < weaponVariants.Count; i++)
            {
                if (weaponVariants[i].Equals(WeaponVariant.None))
                {
                    continue;
                }

                var data = m_WeaponConfigurator.GetData(weaponVariants[i]);

                var builder = new WeaponBuilder(weaponPoints[i])
                                    .SetWeaponData(data.WeaponData)
                                    .SetWeaponBehaviour(data.WeaponBehaviour);

                needingWeapon.Set(builder.Build());
            }
        }

        public void GaveWeapon(WeaponVariant weaponVariant, INeedingWeapon needingWeapon)
        {
            var data = m_WeaponConfigurator.GetData(weaponVariant);

            var builder = new WeaponBuilder(needingWeapon.Where())
                                .SetWeaponData(data.WeaponData)
                                .SetWeaponBehaviour(data.WeaponBehaviour);

            needingWeapon.Set(builder.Build());
            needingWeapon.SetWeaponVariant(weaponVariant);
        }
    }
}