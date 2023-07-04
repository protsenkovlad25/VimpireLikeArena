using System;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Levels;

public class WavesController : MonoBehaviour
{
    public event Action<Chunk> OnSetChunk;
    public event Action<Chunk> OnSpawnPauseEnd;
    public event Action OnAllWavesSpawned;

    [SerializeField] private GameInterfaceManager m_GIM;
    [SerializeField] private Transform m_ChunkParent;
    [SerializeField] private float m_SpawnChunkDelay;


    private List<Chunk> m_Chunks;
    private Chunk m_InstiateChunk;
    private Vector3 m_ArenaCenterPosition;
    private Timer m_TimerToNextWave;

    private bool m_IsPauseSpawn;

    private void Update()
    {
        m_TimerToNextWave?.Update();

        if (m_TimerToNextWave != null && !m_IsPauseSpawn)
            m_GIM.UpdateSpawnChunkTimer((int)(m_TimerToNextWave.currentTime + 1.5));
    }

    public void Init()
    {
        m_IsPauseSpawn = false;
    }

    public void InitializeWaves(List<Chunk> chunks, Vector3 arenaCenterPosition)
    {
        if (m_InstiateChunk != null)
        {
            Destroy(m_InstiateChunk.gameObject);
        }

        //NewTimer();
        PauseSpawn();
        m_GIM.OnTimer();
        m_GIM.ChunkTimerText("New Wave");

        m_Chunks = new List<Chunk>();
        m_Chunks.AddRange(chunks);

        m_ArenaCenterPosition = arenaCenterPosition;

        m_InstiateChunk = Instantiate(m_Chunks[0], m_ArenaCenterPosition, Quaternion.identity, m_ChunkParent);
        m_Chunks.RemoveAt(0);

        OnSetChunk?.Invoke(m_InstiateChunk);
    }

    public void NewTimer()
    {
        m_TimerToNextWave = new Timer(m_SpawnChunkDelay - 1.5f);
        m_TimerToNextWave.OnTimesUp.AddListener(SpawnNextWave);
    }

    public void PauseSpawn()
    {
        m_IsPauseSpawn = true;

        m_TimerToNextWave = new Timer(1.5f);
        m_GIM.TimerBlinking();
        m_TimerToNextWave.OnTimesUp.AddListener(NewTimer);
        m_TimerToNextWave.OnTimesUp.AddListener(PauseSpawnEnd);
    }

    public void PauseSpawnEnd()
    {
        m_IsPauseSpawn = false;
        OnSpawnPauseEnd.Invoke(m_InstiateChunk);
    }

    public void StopTimer()
    {
        m_TimerToNextWave = null;
        m_GIM.OffTimer();
    }

    public void SpawnNextWave()
    {
        if (m_Chunks.Count > 0)
        {
            if (m_Chunks.Count == 1)
            {
                //PauseSpawn();
                m_IsPauseSpawn = true;

                m_TimerToNextWave = new Timer(1.5f);
                m_TimerToNextWave.OnTimesUp.AddListener(PauseSpawnEnd);
                m_TimerToNextWave.OnTimesUp.AddListener(StopTimer);

                m_GIM.TimerBlinking();
                m_GIM.ChunkTimerText("Last Wave");
            }
            else
            {
                PauseSpawn();
                m_GIM.ChunkTimerText("New Wave");
            }

            m_InstiateChunk = Instantiate(m_Chunks[0], m_ArenaCenterPosition, Quaternion.identity, m_ChunkParent);
            m_Chunks.RemoveAt(0);

            OnSetChunk?.Invoke(m_InstiateChunk);
        }
        else
        {
            m_TimerToNextWave = null;
            m_GIM.OffTimer();
            
            OnAllWavesSpawned?.Invoke();
        }
    }
}
