using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Levels;

namespace VampireLike.Core.Trees
{
    public class ArenaNode
    {
        public ArenaNode(WavesCluster wavesCluster, Prize prize = null)
        {
            WavesCluster = wavesCluster;
            Prize = prize;
        }

        public WavesCluster WavesCluster { get; set; }
        public Prize Prize { get; set; }
        public ArenaNode? Next { get; set; }
    }
}