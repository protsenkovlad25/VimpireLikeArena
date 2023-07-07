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
            List<WeaponType> weaponTypes = needingWeapon.GetWeaponTypes();
            List<Transform> weaponPoints = needingWeapon.GetWeaponPoints();

            for (int i = 0; i < weaponTypes.Count; i++)
            {
                if (weaponTypes[i].Equals(WeaponType.None))
                {
                    continue;
                }

                var data = m_WeaponConfigurator.GetData(weaponTypes[i]);

                var builder = new WeaponBuilder(weaponPoints[i])
                                    .SetWeaponData(data.WeaponData)
                                    .SetWeaponBehaviour(data.WeaponBehaviour);

                needingWeapon.Set(builder.Build());
            }
        }

        public void GaveWeapon(WeaponType weaponType, INeedingWeapon needingWeapon)
        {
            var data = m_WeaponConfigurator.GetData(weaponType);

            var builder = new WeaponBuilder(needingWeapon.Where())
                                .SetWeaponData(data.WeaponData)
                                .SetWeaponBehaviour(data.WeaponBehaviour);

            needingWeapon.Set(builder.Build());
            needingWeapon.SetWeaponType(weaponType);
        }
    }
}