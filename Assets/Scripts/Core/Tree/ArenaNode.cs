using VampireLike.Core.Levels;

namespace VampireLike.Core.Trees
{
    public class ArenaNode
    {
        public ArenaNode(WavesCluster wavesCluster, Prize prize)
        {
            WavesCluster = wavesCluster;
            Prize = prize;
        }

        public WavesCluster WavesCluster { get; set; }
        public Prize Prize { get; set; }
        public ArenaNode? Next { get; set; }
    }
}