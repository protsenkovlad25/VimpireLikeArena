using System;
using UnityEngine;
using VampireLike.Core.General;
using VampireLike.Core.Trees;

namespace VampireLike.Core.Levels
{
    public class Level : MonoBehaviour
    {
        public event Action<Chunk> OnSetChunk;
        public event Action<Chunk> OnSpawnPauseEnd;
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

        public void Init()
        {
            m_WavesController.Init();
            m_LevelGenerator.Init();

            m_TreeHolder =  m_LevelGenerator.GenerateTree();

            m_WavesController.OnSetChunk += OnSetChunk.Invoke;
            m_WavesController.OnSpawnPauseEnd += OnSpawnPauseEnd.Invoke;
        }

        public void FirstArena()
        {
            InitializeWaves();
        }

        public void NextArena()
        {
            var road = Instantiate(m_PrefabRoad, m_CurrentArena.EndPoint.position, Quaternion.identity, m_RoadParent);
            var arena = Instantiate(m_PrefabArena, m_CurrentArena.transform.position + Vector3.forward * 54, Quaternion.identity, m_ArenaParent);
            
            arena.OnSteppedArena += RemoveWavesCluster;
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
        }

        public void InitializePrize()
        {
            m_TreeHolder.CurrentArenaNode.Prize.SpawnPrizes(m_CurrentArena.CenterArena.position);
        }

        public void RemoveWavesCluster()
        {
            m_TreeHolder.CurrentArenaNode = m_TreeHolder.CurrentArenaNode.Next;
            //m_TreeHolder.Remove(m_TreeHolder.CurrentArenaNode.WavesCluster, m_TreeHolder.CurrentArenaNode.Prize);
        }

        public WavesCluster GetWavesCluster()
        {
            //m_TreeHolder.CurrentArenaNode = m_TreeHolder.CurrentArenaNode.Next;
            return m_TreeHolder.CurrentArenaNode.WavesCluster;
        }

        public void NextWave()
        {
            m_WavesController.SpawnNextWave();
        }
    }
}