using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Movements;

namespace VampireLike.Core.Characters.Enemies.Config
{
    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Configs/Enemies/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyDataConfig> m_EnemyData;

        public List<EnemyDataConfig> EnemyData => m_EnemyData;
    }

    [System.Serializable]
    public class EnemyDataConfig
    {
        [SerializeField] private string m_Name;

        [SerializeField] private EnemyType m_EnemyType;
        [SerializeField] private int m_HealthPoints;
        [SerializeField] private float m_Speed;
        [SerializeField] private int m_BaseDamage;
        [SerializeField] private EnemyMovementType m_EnemyMovementType;

        public string Name => m_Name;

        public EnemyType EnemyType => m_EnemyType;
        public int HealthPoints => m_HealthPoints;
        public float Speed => m_Speed;
        public int BaseDamage => m_BaseDamage;
        public EnemyMovementType EnemyMovementType => m_EnemyMovementType;
    }
}
