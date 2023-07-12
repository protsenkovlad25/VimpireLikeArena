using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VampireLike.Core.Players;

namespace VampireLike.UI
{
    public class MenuInterfaceManager : MonoBehaviour
    {
        [SerializeField] TMP_Text numLevel;

        public void Init(Player player)
        {
            numLevel.text = player.Level.ToString();
        }

        public void LoadGameScene()
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}
