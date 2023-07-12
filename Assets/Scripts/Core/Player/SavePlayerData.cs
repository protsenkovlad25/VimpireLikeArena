using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace VampireLike.Core.Players
{
    public static class SavePlayerData
    {
        public static void SaveData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/SavePlayerData.dat");

            bf.Serialize(file, PlayerController.Instance.Player);
            file.Close();

            Debug.Log("Player data is saved!");
        }

        public static Player LoadData()
        {
            if (File.Exists(Application.persistentDataPath + "/SavePlayerData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SavePlayerData.dat", FileMode.Open);

                Player playerData = (Player)bf.Deserialize(file);
                file.Close();

                Debug.Log("Player data is loaded!");

                return playerData;
            }
            else
                return null;
        }
    }
}
