using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Players;
using VampireLike.Core.Trees;

namespace VampireLike.Core.Levels
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private WavesConfigurator m_WavesConfigurator;

        System.Random random;
        [SerializeField] GameObject m_PickupbleWeaponPrefab;
        [SerializeField] GameObject m_PickupbleItemPrefab;

        public void Init()
        {
            random = new System.Random(PlayerController.Instance.Player.Seed);
            m_WavesConfigurator.Init();
        }

        public WavesCluster GenerateWavesCluster()
        {
            WavesCluster wavesCluster = m_WavesConfigurator.GetRandomWavesCluster(m_WavesConfigurator.GetTier(random), random);


            m_WavesConfigurator.Overflow(PlayerController.Instance.Player.QtyCompleteArean - 1, PlayerController.Instance.Player.QtyArenas - 1);

            return wavesCluster;
        }

        public Prize GeneratePrize()
        {
            return new Prize(m_PickupbleWeaponPrefab, 3);
        }

        public TreeHolder GenerateTree()
        {
            TreeHolder treeHolder = new TreeHolder { Count = 5 };

            for (int i = 0; i < treeHolder.Count; i++)
            {
                treeHolder.Add(GenerateWavesCluster(), GeneratePrize());
            }

            //ArenaNode node;
            //for (node = treeHolder.CurrentArenaNode; node != null; node = node.Next)
            //    Debug.Log(node.WavesCluster.Chunks.Count + "chunks");

            return treeHolder;
        }
    }
}
