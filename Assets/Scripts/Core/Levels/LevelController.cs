using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Players;

namespace VampireLike.Core.Levels
{
    public class LevelController : MonoBehaviour
    {
        public event Action<Chunk> OnSetChunk;

        [SerializeField] private Level m_Level;
        [SerializeField] private ChunkConfigurator m_ChunkConfigurator;

        public void Init()
        {
            m_ChunkConfigurator.Init();
            m_Level.OnSetChunk += OnSetChunk.Invoke;
        }

        public void FirstArena()
        {
            SetChunk(m_ChunkConfigurator.GetRandomChunk(1, PlayerController.Instance.Player.Seed));
            m_Level.InstallCurrentChunk();
        }

        public void NextArena()
        {
            int seed = PlayerController.Instance.Player.Seed;

            m_ChunkConfigurator.Overflow(PlayerController.Instance.Player.Node, PlayerController.Instance.Player.QtyArenas - 1);
            m_ChunkConfigurator.Show();
            m_Level.NextArena();
            SetChunk(m_ChunkConfigurator.GetRandomChunk(m_ChunkConfigurator.GetTier(seed), seed));
        }

        public void SetChunk(Chunk chunk)
        {
            m_Level.SetChunk(chunk);
        }
    }
}