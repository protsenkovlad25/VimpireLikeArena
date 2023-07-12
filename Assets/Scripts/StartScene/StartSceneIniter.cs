using UnityEngine;
using VampireLike.Core.Players;
using VampireLike.UI;

namespace VampireLike.StartScenes
{
    public class StartSceneIniter : MonoBehaviour
    {
        [SerializeField] private PlayerController m_PlayerController;
        [SerializeField] private MenuInterfaceManager m_MenuInterfaceManager;

        private void Awake()
        {
            m_PlayerController.Init();
            m_MenuInterfaceManager.Init(m_PlayerController.Player);
        }
    }
}