using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    public class ItemSynergyType : ItemObject
    {
        public override void Init()
        {
            base.Init();

            m_ItemType = ItemType.SynergyType;
        }
    }
}
