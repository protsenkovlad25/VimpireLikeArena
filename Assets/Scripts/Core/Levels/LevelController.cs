using System;
using System.Collections;
using UnityEngine;
using VampireLike.Core.Players;

namespace VampireLike.Core.Levels
{
    public class LevelController : MonoBehaviour
    {
        public event Action<Chunk> OnSetChunk;

        [SerializeField] private Level m_Level;
        [SerializeField] private ChunkConfigurator m_ChunkConfigurator;
        [SerializeField] private WavesConfigurator m_WavesConfigurator;
        
        [SerializeField] private float m_SpawnChunkDelay;

        private WavesCluster m_WavesCluster;
        private float m_CurrentTime;
        private bool m_IsFight;
        private int m_CurrentChunkNumber;

        public bool IsFight => m_IsFight;

        System.Random random;

        public void Init()
        {
            random = new System.Random(PlayerController.Instance.Player.Seed);
            m_ChunkConfigurator.Init();
            m_WavesConfigurator.Init();
            m_Level.OnSetChunk += OnSetChunk.Invoke;
            m_Level.OnStartFight += StartFight;

            m_CurrentChunkNumber = 0;
            m_IsFight = true;
        }

        public void Update()
        {
            if (m_IsFight)
            {
                if (m_CurrentChunkNumber != m_WavesCluster.Chunks.Count)
                {
                    if (m_CurrentTime <= 0)
                    {
                        Debug.Log(m_CurrentTime);
                        m_CurrentChunkNumber++;
                        SetChunk(m_WavesCluster.Chunks[m_CurrentChunkNumber]);
                        m_Level.InstallCurrentChunk();
                        m_CurrentTime = m_SpawnChunkDelay;
                    }
                    m_CurrentTime -= Time.deltaTime;
                }
                else
                    m_IsFight = false;
            }
        }

        public void FirstArena()
        {
            m_WavesCluster = m_WavesConfigurator.GetRandomWavesCluster(1, random);
            SetChunk(m_WavesCluster.Chunks[m_CurrentChunkNumber]);

            //SetChunk(m_ChunkConfigurator.GetRandomChunk(1, random));
            m_Level.InstallCurrentChunk();

            m_IsFight = true;
            m_CurrentTime = m_SpawnChunkDelay;
        }

        public void NextArena()
        {
            int seed = PlayerController.Instance.Player.Seed;

            //m_ChunkConfigurator.Overflow(PlayerController.Instance.Player.QtyCompleteArean - 1, PlayerController.Instance.Player.QtyArenas - 1);
            //m_ChunkConfigurator.Show();

            m_WavesConfigurator.Overflow(PlayerController.Instance.Player.QtyCompleteArean - 1, PlayerController.Instance.Player.QtyArenas - 1);

            m_Level.NextArena();
            m_WavesCluster = m_WavesConfigurator.GetRandomWavesCluster(m_WavesConfigurator.GetTier(seed), random);
            m_CurrentChunkNumber = 0;
            SetChunk(m_WavesCluster.Chunks[m_CurrentChunkNumber]);

            //SetChunk(m_ChunkConfigurator.GetRandomChunk(m_ChunkConfigurator.GetTier(seed), random));
        }

        public void StartNextChunk()
        {
            StartCoroutine(SpawnDelay());
        }

        public void SetChunk(Chunk chunk)
        {
            m_Level.SetChunk(chunk);
        }

        public void StartFight()
        {
            m_IsFight = true;
            m_CurrentTime = m_SpawnChunkDelay;
        }

        private IEnumerator SpawnDelay()
        {
            m_IsFight = false;
            m_CurrentTime = 0;

            yield return new WaitForSeconds(1.5f);

            m_IsFight = true;
        }
    }
}