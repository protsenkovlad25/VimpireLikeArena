using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using VampireLike.Core.Players;

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
