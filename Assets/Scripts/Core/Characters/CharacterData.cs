using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Characters
{
    [SerializeField]
    public class CharacterData
    {

        [Header("Only view")]
        [SerializeField] private int m_HealthPoints;
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_ScaleDamage;

        public float Speed { get => m_Speed; set => m_Speed = value; }
        public int HealthPoints { get => m_HealthPoints; set => m_HealthPoints = value; }
        public float ScaleDamage { get => m_ScaleDamage; set => m_ScaleDamage = value; }

    }
}