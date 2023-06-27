using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Players;

namespace VampireLike.Core.Levels
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private WavesConfigurator m_WavesConfigurator;

        System.Random random;

        public void Init()
        {
            random = new System.Random(PlayerController.Instance.Player.Seed);
            m_WavesConfigurator.Init();
        }

        public List<Chunk> GenerateWavesCluster(int seed)
        {
            WavesCluster wavesCluster = m_WavesConfigurator.GetRandomWavesCluster(m_WavesConfigurator.GetTier(seed), random);

            List<Chunk> ñhunks = new List<Chunk>();
            foreach (var chunk in wavesCluster.Chunks)
            {
                ñhunks.Add(chunk);
            }

            m_WavesConfigurator.Overflow(PlayerController.Instance.Player.QtyCompleteArean - 1, PlayerController.Instance.Player.QtyArenas - 1);

            return ñhunks;
        } 
    }
}
