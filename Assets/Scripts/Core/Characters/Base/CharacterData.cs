using UnityEngine;

namespace VampireLike.Core.Characters
{
    [SerializeField]
    public class CharacterData
    {

        [Header("Only view")]
        [SerializeField] private int m_HealthPoints;
        [SerializeField] private float m_Speed;
        [SerializeField] private int m_BaseDamage;

        public float Speed { get => m_Speed; set => m_Speed = value; }
        public int HealthPoints { get => m_HealthPoints; set => m_HealthPoints = value; }
        public int BaseDamage { get => m_BaseDamage; set => m_BaseDamage = value; }

    }
}