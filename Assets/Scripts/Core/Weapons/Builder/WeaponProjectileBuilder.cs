using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public class WeaponBuilder : IBuilder<WeaponBehaviour>
    {
        private Transform m_Point;
        private WeaponBehaviour m_WeaponBehaviour;
        private WeaponData m_WeaponData;

        public WeaponBuilder(Transform point)
        {
            m_Point = point;
        }

        public WeaponBuilder SetWeaponData(WeaponData weaponData)
        {
            m_WeaponData = weaponData;
            return this;
        }

        public WeaponBuilder SetWeaponBehaviour(WeaponBehaviour behaviour)
        {
            m_WeaponBehaviour = behaviour;
            return this;
        }

        public WeaponBehaviour Build()
        {
            var go = GameObject.Instantiate(m_WeaponBehaviour, m_Point.position, m_Point.rotation, m_Point);

            go.SetWeaponData(m_WeaponData);

            return go;
        }
    }
}