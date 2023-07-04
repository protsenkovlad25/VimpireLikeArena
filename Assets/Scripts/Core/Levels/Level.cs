using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Players;
using VampireLike.Core.Trees;

namespace VampireLike.Core.Levels
{

    public class Level : MonoBehaviour
    {
        public event Action<Chunk> OnSetChunk;
        public event Action<Chunk> OnSpawnPauseEnd;
        public event Action OnArenaIsCleared;
        public event Action OnStartFight;

        [SerializeField] private WavesController m_WavesController;
        [SerializeField] private LevelGenerator m_LevelGenerator;

        [SerializeField] private Arena m_PrefabArena;
        [SerializeField] private Road m_PrefabRoad;

        [SerializeField] private Arena m_CurrentArena;

        [Header("Parent")]
        [SerializeField] private Transform m_RoadParent;
        [SerializeField] private Transform m_ArenaParent;

        private TreeHolder m_TreeHolder;
        private int m_Seed;

        public void Init()
        {
            m_Seed = PlayerController.Instance.Player.Seed;
            m_WavesController.Init();
            m_LevelGenerator.Init();

            m_TreeHolder =  m_LevelGenerator.GenerateTree();

            m_WavesController.OnSetChunk += OnSetChunk.Invoke;
            m_WavesController.OnSpawnPauseEnd += OnSpawnPauseEnd.Invoke;
            m_WavesController.OnAllWavesSpawned += OnArenaIsCleared.Invoke;
        }

        public void FirstArena()
        {
            InitializeWaves();
        }

        public void NextArena()
        {
            var road = Instantiate(m_PrefabRoad, m_CurrentArena.EndPoint.position, Quaternion.identity, m_RoadParent);
            var arena = Instantiate(m_PrefabArena, m_CurrentArena.transform.position + Vector3.forward * 54, Quaternion.identity, m_ArenaParent);
            
            arena.OnSteppedArena += InitializeWaves;
            arena.OnSteppedArena += EventManager.StartArena;

            arena.gameObject.SetActive(false);
            arena.transform.localScale = Vector3.zero;
            arena.gameObject.SetActive(true);

            road.gameObject.SetActive(false);
            road.transform.position = road.transform.position - Vector3.up * 10;
            road.gameObject.SetActive(true);

            road.Move(Vector3.up * 10);
            arena.Scale(Vector3.one);

            m_CurrentArena = arena;
        }

        public void InitializeWaves()
        {
            m_WavesController.InitializeWaves(GetWavesCluster().Chunks, m_CurrentArena.CenterArena.position);

            m_TreeHolder.Remove(m_TreeHolder.CurrentArenaNode.WavesCluster);
        }

        public WavesCluster GetWavesCluster()
        {
            return m_TreeHolder.CurrentArenaNode.WavesCluster;
        }

        public void NextWave()
        {
            m_WavesController.SpawnNextWave();
        }
    }
}