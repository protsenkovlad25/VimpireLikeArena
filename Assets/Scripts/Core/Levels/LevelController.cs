using System;
using System.Collections;
using UnityEngine;
using VampireLike.Core.Players;
using DG.Tweening;

namespace VampireLike.Core.Levels
{
    public class LevelController : MonoBehaviour
    {
        public event Action<Chunk> OnSetChunk;
        public event Action OnAllWavesSpawned;

        [SerializeField] private Level m_Level;
        [SerializeField] private ChunkConfigurator m_ChunkConfigurator;
        [SerializeField] private WavesConfigurator m_WavesConfigurator;
        [SerializeField] private GameInterfaceManager m_GIM;
        
        [SerializeField] private float m_SpawnChunkDelay;

        private WavesCluster m_WavesCluster;
        private float m_CurrentTime;
        private bool m_IsFight;
        private bool m_PauseSpawn;
        private bool m_TimerEnd;
        private int m_CurrentChunkNumber;

        public bool IsFight => m_IsFight;
        public bool PauseSpawn
        {
            get => m_PauseSpawn;
            set => m_PauseSpawn = value;
        }

        System.Random random;

        public void Init()
        {
            random = new System.Random(PlayerController.Instance.Player.Seed);
            m_ChunkConfigurator.Init();
            m_WavesConfigurator.Init();
            m_Level.OnSetChunk += OnSetChunk.Invoke;
            m_Level.OnStartFight += StartFight;

            m_CurrentChunkNumber = 0;
            m_IsFight = false;
            m_TimerEnd = false;
            m_PauseSpawn = false;
        }

        public void Update()
        {
            if (m_IsFight)
            {
                if (m_CurrentChunkNumber + 1 != m_WavesCluster.Chunks.Count)
                {
                    if (m_CurrentTime <= 1.5f && m_TimerEnd == false)
                    {
                        InstallNextChunk();
                        m_TimerEnd = true;
                        m_PauseSpawn = true;
                    }
                    else if (m_CurrentTime <= 0)
                    {
                        m_CurrentTime = m_SpawnChunkDelay;
                        m_TimerEnd = false;
                        m_PauseSpawn = false;
                    }
                    m_CurrentTime -= Time.deltaTime;
                    UpdateTimer();
                }
                else if (m_PauseSpawn == false)
                {

                    m_GIM.ChunkTimerText("Last Wave");
                }
            }
        }

        public void FirstArena()
        {
            m_WavesCluster = m_WavesConfigurator.GetRandomWavesCluster(1, random);
            SetChunk(m_WavesCluster.Chunks[m_CurrentChunkNumber]);

            //SetChunk(m_ChunkConfigurator.GetRandomChunk(1, random));

            m_Level.InstallCurrentChunk();

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

        public void InstallNextChunk()
        {
            m_CurrentChunkNumber++;
            SetChunk(m_WavesCluster.Chunks[m_CurrentChunkNumber]);
            m_Level.InstallCurrentChunk();
        }

        public void IsCompleteWavesCluster()
        {
            if (m_CurrentChunkNumber + 1 == m_WavesCluster.Chunks.Count)
            {
                m_IsFight = false;
                m_CurrentTime = 0;
                m_GIM.OffTimer();
                OnAllWavesSpawned?.Invoke();
            }
            else
            {
                m_CurrentTime = 1.5f;
                m_TimerEnd = false;
                m_PauseSpawn = false;

            }
        }

        public void SetChunk(Chunk chunk)
        {
            m_Level.SetChunk(chunk);
        }

        public void StartFight()
        {
            m_IsFight = true;
            m_TimerEnd = false;
            m_GIM.OnTimer();
            m_CurrentTime = m_SpawnChunkDelay;
            m_GIM.UpdateSpawnChunkTimer((int)m_CurrentTime);
        }

        public void UpdateTimer()
        {
            if (m_PauseSpawn)
            {
                m_GIM.ChunkTimerText("New Wave");
                //m_GIM.TimerBlinking();
            }
            else
                m_GIM.UpdateSpawnChunkTimer((int)m_CurrentTime);
        }
    }
}