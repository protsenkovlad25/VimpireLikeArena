using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.Characters.Enemies;

namespace VampireLike.Core.Levels
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private int m_Tier;
        [SerializeField] private List<EnemyCharacter> m_Enemies;

        public int Tier => m_Tier;
        public List<EnemyCharacter> Enemies => m_Enemies;
    }
}