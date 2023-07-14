using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Players;
using VampireLike.Core.Trees;
using VampireLike.General;

namespace VampireLike.Core.Levels
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private WavesConfigurator m_WavesConfigurator;

        [SerializeField] private GameObject m_ItemPrizePrefab;
        [SerializeField] private GameObject m_WeapomPrizePrefab;

        System.Random random;

        public void Init()
        {
            random = new System.Random(PlayerController.Instance.Player.Seed);
            m_WavesConfigurator.Init();
        }

        public WavesCluster GenerateWavesCluster()
        {
            random = new System.Random(PlayerController.Instance.Player.Seed);
            WavesCluster wavesCluster = m_WavesConfigurator.GetRandomWavesCluster(m_WavesConfigurator.GetTier(random), random);


            m_WavesConfigurator.Overflow(PlayerController.Instance.Player.QtyCompleteArean - 1, PlayerController.Instance.Player.QtyArenas - 1);
            
            return wavesCluster;
        }

        public Prize GeneratePrize()
        {
            random = new System.Random();

            int randomPrize = Random.value > .5f ? 1 : 0;
            int countPrize = random.Next(1, 4);

            return randomPrize switch
            {
                0 => new Prize(m_WeapomPrizePrefab, countPrize).InitializeWeaponPrizes(),
                1 => new Prize(m_ItemPrizePrefab, countPrize).InitializeItemPrizes()
            };

            // -- Weapon -- //
            //return new Prize(m_PickupbleWeaponPrefab, 3);

            // -- Item -- //
            //return new Prize(m_PickupbleWeaponPrefab, 3);
        }

        public TreeHolder GenerateTree()
        {
            TreeHolder treeHolder = new TreeHolder(5);

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
