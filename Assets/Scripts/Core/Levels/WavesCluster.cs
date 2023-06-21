using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    public class WavesCluster : MonoBehaviour
    {
        public List<Chunk> Chunks { get; set; }
        public int Tier { get; set; }
    }
}
