using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Levels;

namespace VampireLike.Core.Trees
{
    public class TreeHolder
    {
        ArenaNode currentArena;
        ArenaNode lastArena;
        int count;

        public int Count 
        { 
            get { return count; } 
            set { count = value; } 
        }
        public ArenaNode CurrentArenaNode
        {
            get { return currentArena; }
        }

        public ArenaNode GetCurrentArena()
        {
            return currentArena;
        }

        public void Add(WavesCluster wavesCluster)
        {
            ArenaNode newArenaNode = new ArenaNode(wavesCluster);

            if (currentArena == null)
                currentArena = newArenaNode;
            else
                lastArena.Next = newArenaNode;
                
            lastArena = newArenaNode;
        }

        public bool Remove(WavesCluster wavesCluster)
        {
            ArenaNode current = currentArena;
            ArenaNode previous = null;
            if (currentArena.WavesCluster == null) Debug.Log("Piska");

            while (current != null && current.WavesCluster != null)
            {
                Debug.Log("LOL");
                if (current.WavesCluster.Equals(wavesCluster))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current.Next == null)
                            lastArena = previous;
                    }
                    else
                    {
                        currentArena = currentArena?.Next;

                        if (currentArena == null)
                            lastArena = null;
                    }
                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }

        public void Clear()
        {
            currentArena = null;
            lastArena = null;
            count = 0;
        }
    }
}