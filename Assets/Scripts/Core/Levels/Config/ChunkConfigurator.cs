using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace VampireLike.Core.Levels
{
    public class ChunkConfigurator : MonoBehaviour
    {
        [SerializeField] private ChunkConfig m_ChunkConfig;

        private Dictionary<int, List<Chunk>> m_Chunks;
        private List<ChunkTier> m_ChunkTiers;

        public void Init()
        {
            m_Chunks = new Dictionary<int, List<Chunk>>();

            m_ChunkTiers = new List<ChunkTier>();

            foreach (var item in m_ChunkConfig.Chunks)
            {
                if (m_Chunks.ContainsKey(item.Tier))
                {
                    m_Chunks[item.Tier].Add(item);
                }
                else
                {
                    m_Chunks.Add(item.Tier, new List<Chunk>());
                    m_Chunks[item.Tier].Add(item);
                }
            }

            foreach (var item in m_ChunkConfig.ChunkTierConfig.StartDataChunks)
            {
                m_ChunkTiers.Add(new ChunkTier()
                {
                    Tier = item.Tier,
                    Percent = item.Percent
                });
            }
        }
        
        public int GetTier(int seed)
        {
            var random = new System.Random(seed);

            var list = new List<int>();

            foreach (var item in m_ChunkTiers)
            {
                for (int i = 0; i < item.Percent; i++)
                {
                    list.Add(item.Tier);
                }
            }

            int result = list[random.Next(0, list.Count)];

            return result;
        }

        public Chunk GetRandomChunk(int tier, int seed)
        {
            var random = new System.Random(seed);

            int index = random.Next(0, m_Chunks[tier].Count);

            return m_Chunks[tier][index];
        }

        public void Overflow(int currentNode, int maxNode)
        {
            int procent = PowerOverflowing(maxNode, m_ChunkConfig.ChunkTierConfig.PrecentageOverflow);
            Debug.LogError(procent);
            FlowPercentage(procent, m_ChunkTiers, currentNode, maxNode);
        }

        public void Show()
        {
            Debug.Log(StringTier());
        }

        private string StringTier()
        {
            var strBuild = new StringBuilder();

            foreach (var item in m_ChunkTiers)
            {
                strBuild.Append(item.Tier)
                        .Append(' ')
                        .Append('-')
                        .Append(' ')
                        .Append(item.Percent)
                        .Append('.')
                        .Append('\n');
            }

            return strBuild.ToString();
        }

        //TODO Кринж но всё по ТЗ
        private void FlowPercentage(int percent, List<ChunkTier> tiers, int currentNode, int maxNode)
        {
            if (currentNode < maxNode/2)
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