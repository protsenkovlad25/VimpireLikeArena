using UnityEngine;

namespace VampireLike.Core.Players
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        public Player Player;

        public void Init()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this.gameObject);

                Instance = this;
            }
            Player = SavePlayerData.LoadData();

            if (Player == null)
            {
                Player = new Player();

                Player.Node = 0;
                Player.QtyCompleteArean = 0;
                Player.Level = 1;
                Player.Seed = Player.Level;
                Player.QtyArenas = 5;
            }
        }

        public void StartRoad()
        {
            Player.Node++;
        }

        public void CompleteArena()
        {
            Player.QtyCompleteArean++;
        }

        public void CompleteLevel()
        {
            Player.QtyCompleteArean = 0;
            Player.Level++;
            Player.Seed = Player.Level;
            Player.Node = 0;
        }

        public bool IsCompleteLevel()
        {
            return Player.QtyCompleteArean > Player.QtyArenas;
        }

        public void LoseLevel()
        {
            Player.QtyCompleteArean = 0;
        }
    }
}