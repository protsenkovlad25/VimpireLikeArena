using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    [CreateAssetMenu(fileName = "Chunk Config", menuName = "Configs/Levels/Chunk Config")]
    public class ChunkConfig : ScriptableObject
    {
        [SerializeField] private List<Chunk> m_Chunks;
        [SerializeField] private ChunkTierConfig m_ChunkTierConfig;

        public List<Chunk> Chunks => m_Chunks;
        public ChunkTierConfig ChunkTierConfig => m_ChunkTierConfig;
    }

    [System.Serializable]
    public class ChunkTierConfig
    {
        [SerializeField] private int m_PrecentageOverflow;
        [SerializeField] private List<StartDataChunk> m_StartDataChunks;

        public int PrecentageOverflow => m_PrecentageOverflow;
        public List<StartDataChunk> StartDataChunks => m_StartDataChunks;

        [System.Serializable]
        public struct StartDataChunk
        {
            [SerializeField] private int m_Tier;
            [SerializeField] private int m_Percent;

            public int Tier => m_Tier;
            public int Percent => m_Percent;
        }
    }

    [System.Serializable]
    public class ChunkTier
    {
        public int Tier { get; set; }
        public int Percent { get; set; }
    }
}