using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters.Enemies;

namespace VampireLike.Core.Levels
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private int m_Tier;

        public int Tier => m_Tier;
        public List<EnemyCharacter> Enemies 
        { 
            get 
            {
                List<EnemyCharacter> result = new List<EnemyCharacter>();
                foreach (Transform child in transform)
                    if (child.TryGetComponent(out EnemyCharacter ec))
                        result.Add(ec);

                return result;
            } 
        }

        public List<SolidObject> Walls
        {
            get
            {
                List<SolidObject> walls = new List<SolidObject>();
                foreach (Transform child in transform)
                    if (child.TryGetComponent(out SolidObject solid))
                        walls.Add(solid);

                return walls;
            }
        }
    }
}