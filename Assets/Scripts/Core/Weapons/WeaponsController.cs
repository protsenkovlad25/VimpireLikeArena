using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Weapons
{

    public class WeaponsController : MonoBehaviour
    {
        [SerializeField] private WeaponConfigurator m_WeaponConfigurator;

        private List<WeaponTypeDataHolder> m_WeaponTypeDataHolders;
        private List<WeaponClassDataHolder> m_WeaponClassDataHolders;

        public List<WeaponTypeDataHolder> WeaponTypeDataHolders 
        { 
            get => m_WeaponTypeDataHolders;
            set => m_WeaponTypeDataHolders = value;
        }
        public List<WeaponClassDataHolder> WeaponClassDataHolders
        {
            get => m_WeaponClassDataHolders;
            set => m_WeaponClassDataHolders = value;
        }

        public void Init()
        {
            EventManager.OnWeaponReceived.AddListener(GaveWeapon);
        }

        public void UploadWeaponTypesAndClasses()
        {
            InitTypesData();
            InitClassesData();
        }

        public void InitTypesData()
        {
            m_WeaponTypeDataHolders = m_WeaponConfigurator.GetTypeDataHolders();
        }

        public void InitClassesData()
        {
            m_WeaponClassDataHolders = m_WeaponConfigurator.GetClassDataHolders();
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
                                    .SetWeaponBehaviour(data.WeaponBehaviour)
                                    .SetWeaponTypeData(m_WeaponTypeDataHolders.Find(item => item.WeaponType.Equals(data.WeaponData.WeaponType)))
                                    .SetWeaponClassData(m_WeaponClassDataHolders.Find(item => item.WeaponClass.Equals(data.WeaponData.WeaponClass)));
                
                needingWeapon.Set(builder.Build());
            }
        }

        public void GaveWeapon(WeaponVariant weaponVariant, INeedingWeapon needingWeapon)
        {
            var data = m_WeaponConfigurator.GetData(weaponVariant);

            var builder = new WeaponBuilder(needingWeapon.Where())
                                .SetWeaponData(data.WeaponData)
                                .SetWeaponBehaviour(data.WeaponBehaviour)
                                .SetWeaponTypeData(m_WeaponTypeDataHolders.Find(item => item.WeaponType.Equals(data.WeaponData.WeaponType)))
                                .SetWeaponClassData(m_WeaponClassDataHolders.Find(item => item.WeaponClass.Equals(data.WeaponData.WeaponClass)));

            needingWeapon.Set(builder.Build());
            needingWeapon.SetWeaponVariant(weaponVariant);
        }
    }
}