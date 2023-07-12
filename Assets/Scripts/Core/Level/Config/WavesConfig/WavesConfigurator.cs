using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    public class WavesConfigurator : MonoBehaviour
    {
        [SerializeField] private WavesConfig m_WavesConfig;

        private Dictionary<int, List<WavesCluster>> m_WavesClusters;
        private List<WavesClusterTier> m_WavesClusterTiers;

        public void Init()
        {
            m_WavesClusters = new Dictionary<int, List<WavesCluster>>();

            m_WavesClusterTiers = new List<WavesClusterTier>();

            foreach (var item in m_WavesConfig.WaveClusterKits)
            {
                if (m_WavesClusters.ContainsKey(item.Tier))
                {
                    WavesCluster newWavesCluster = new WavesCluster()
                    {
                        Chunks = item.Chunks,
                        Tier = item.Tier
                    };
                    m_WavesClusters[item.Tier].Add(newWavesCluster);
                }
                else
                {
                    WavesCluster newWavesCluster = new WavesCluster()
                    {
                        Chunks = item.Chunks,
                        Tier = item.Tier
                    };

                    m_WavesClusters.Add(item.Tier, new List<WavesCluster>());
                    m_WavesClusters[item.Tier].Add(newWavesCluster);
                }
            }

            foreach (var item in m_WavesConfig.WavesClusterTierConfig.StartDataWavesClusters)
            {
                m_WavesClusterTiers.Add(new WavesClusterTier()
                {
                    Tier = item.Tier,
                    Percent = item.Percent
                });
            }
        }

        public int GetTier(System.Random random)
        {
            var list = new List<int>();

            foreach (var item in m_WavesClusterTiers)
            {
                for (int i = 0; i < item.Percent; i++)
                {
                    list.Add(item.Tier);
                }
            }

            int result = list[random.Next(0, list.Count)];

            return result;
        }

        public WavesCluster GetRandomWavesCluster(int tier, System.Random random)
        {
            int index = random.Next(0, m_WavesClusters[tier].Count);

            return m_WavesClusters[tier][index];
        }

        public void Overflow(int currentNode, int maxNode)
        {
            int procent = PowerOverflowing(maxNode, m_WavesConfig.WavesClusterTierConfig.PrecentageOverflow);
            
            FlowPercentage(procent, m_WavesClusterTiers, currentNode, maxNode);
        }

        private void FlowPercentage(int percent, List<WavesClusterTier> tiers, int currentNode, int maxNode)
        {
            if (currentNode <= maxNode / 2)
            {
                var tier1 = tiers.Find(item => item.Tier == 1);
                tier1.Percent -= percent;
                var tier2 = tiers.Find(item => item.Tier == 2);
                tier2.Percent += percent;
            }
            else
            {
                var tier1 = tiers.Find(item => item.Tier == 1);
                tier1.Percent -= percent;
                var tier2 = tiers.Find(item => item.Tier == 2);
                tier2.Percent -= percent;
                var tier3 = tiers.Find(item => item.Tier == 3);
                tier3.Percent += percent * 2;
            }
        }

        private int PowerOverflowing(int numberNode, int percent)
        {
            return percent / numberNode;
        }
    }
}
