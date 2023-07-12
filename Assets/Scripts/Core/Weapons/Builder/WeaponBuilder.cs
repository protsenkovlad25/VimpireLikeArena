using UnityEngine;

namespace VampireLike.Core.Weapons
{
    public class WeaponBuilder : IBuilder<WeaponBehaviour>
    {
        private Transform m_Point;
        private WeaponBehaviour m_WeaponBehaviour;
        private WeaponData m_WeaponData;
        private WeaponTypeDataHolder m_WeaponTypeData;
        private WeaponClassDataHolder m_WeaponClassData;

        public WeaponBuilder(Transform point)
        {
            m_Point = point;
        }

        public WeaponBuilder SetWeaponTypeData(WeaponTypeDataHolder typeData)
        {
            m_WeaponTypeData = typeData;
            return this;
        }

        public WeaponBuilder SetWeaponClassData(WeaponClassDataHolder classData)
        {
            m_WeaponClassData = classData;
            return this;
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
            var go = Object.Instantiate(m_WeaponBehaviour, m_Point.position, m_Point.rotation, m_Point);

            go.SetWeaponData(m_WeaponData);
            go.SetType(m_WeaponTypeData);
            go.SetClass(m_WeaponClassData);

            return go;
        }
    }
}