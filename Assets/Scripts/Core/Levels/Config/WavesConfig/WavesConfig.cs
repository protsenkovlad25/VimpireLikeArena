using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    [CreateAssetMenu(fileName = "Waves Config", menuName = "Configs/Levels/Waves Config")]
    public class WavesConfig : ScriptableObject
    {
        [SerializeField] private List<WavesClusterKit> m_WavesClusterKits;
        [SerializeField] private WavesClusterTierConfig m_WavesClusterTierConfig;

        public List<WavesClusterKit> WaveClusterKits => m_WavesClusterKits;
        public WavesClusterTierConfig WavesClusterTierConfig => m_WavesClusterTierConfig;
    }

    [System.Serializable]
    public class WavesClusterKit
    {
        [SerializeField] private string m_Name;
        [SerializeField] private int m_Tier;
        [SerializeField] private List<Chunk> m_Chunks;

        public string Name => m_Name;
        public int Tier => m_Tier;
        public List<Chunk> Chunks => m_Chunks;
    }

    [System.Serializable]
    public class WavesClusterTierConfig
    {
        [SerializeField] private int m_PrecentageOverflow;
        [SerializeField] private List<StartDataWavesCluster> m_StartDataWavesClusters;

        public int PrecentageOverflow => m_PrecentageOverflow;
        public List<StartDataWavesCluster> StartDataWavesClusters => m_StartDataWavesClusters;

        [System.Serializable]
        public struct StartDataWavesCluster
        {
            [SerializeField] private int m_Tier;
            [SerializeField] private int m_Percent;

            public int Tier => m_Tier;
            public int Percent => m_Percent;
        }
    }

    [System.Serializable]
    public class WavesClusterTier
    {
        public int Tier { get; set; }
        public int Percent { get; set; }
    }
}
