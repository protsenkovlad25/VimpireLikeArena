using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{

    public class Level : MonoBehaviour
    {
        public event Action<Chunk> OnSetChunk;

        [SerializeField] private Arena m_PrefabArena;
        [SerializeField] private Road m_PrefabRoad;

        [SerializeField] private Arena m_CurrentArena;
        [SerializeField] private Chunk m_CurrentChunkPrefab;

        [Header("Parent")]
        [SerializeField] private Transform m_RoadParent;
        [SerializeField] private Transform m_ArenaParent;
        [SerializeField] private Transform m_ChunkParent;

        private Chunk m_InstiateChunk;

        public void NextArena()
        {
            if (m_InstiateChunk != null)
            {
                Destroy(m_InstiateChunk.gameObject);
            }

            var road = Instantiate(m_PrefabRoad, m_CurrentArena.EndPoint.position, Quaternion.identity, m_RoadParent);
            var arena = Instantiate(m_PrefabArena, m_CurrentArena.transform.position + Vector3.forward * 54, Quaternion.identity, m_ArenaParent);
            
            arena.OnSteppedArena += InstallCurrentChunk;

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

        public void SetChunk(Chunk chunkPrefab)
        {
            m_CurrentChunkPrefab = chunkPrefab;
        }

        public void InstallCurrentChunk()
        {
            m_InstiateChunk = Instantiate(m_CurrentChunkPrefab, m_CurrentArena.CenterArena.position, Quaternion.identity, m_ChunkParent);
            OnSetChunk?.Invoke(m_InstiateChunk);
        }
    }
}