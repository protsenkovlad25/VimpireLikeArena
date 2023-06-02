using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Characters
{
    [SerializeField]
    public class CharacterData
    {

        [Header("Only view")]
        [SerializeField] private float m_Speed;
        [SerializeField] private int m_HealthPoints;
        [SerializeField] private float m_ScaleDamge;

        public float Speed { get => m_Speed; set => m_Speed = value; }
        public int HealthPoints { get => m_HealthPoints; set => m_HealthPoints = value; }
        public float ScaleDamage { get => m_ScaleDamge; set => m_ScaleDamge = value; }

    }
}