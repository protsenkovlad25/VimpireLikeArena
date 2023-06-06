using System.Collections;
using UnityEngine;
using VampireLike.Core.Characters;
using VampireLike.Core.Characters.Enemies;
using VampireLike.Core.Input;
using VampireLike.Core.Levels;
using VampireLike.Core.Players;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.General
{

    public class ControllersManager : MonoBehaviour
    {
        [SerializeField] private EnemeisController m_EnemeisController;
        [SerializeField] private PlayerInput m_PlayerInput;
        [SerializeField] private MainCharacterController m_MainCharacterController;
        [SerializeField] private WeaponsController m_WeaponsController;
        [SerializeField] private LevelController m_LevelController;
        [SerializeField] private MISCController m_MISCController;

        public void ControllersInit()
        {
            m_EnemeisController.SetAttaching(m_MainCharacterController);
            m_MainCharacterController.SetAttaching(m_EnemeisController);
            m_WeaponsController.GaveWeapon(m_MainCharacterController);

            m_PlayerInput.OnInput += OnDragJoystickPlayer;
            m_EnemeisController.OnAllDeadEnemies += OnAllDeadEnemies;
            m_LevelController.OnSetChunk += OnSetChunk;

            EventManager.OnLose.AddListener(OnPlayerDied);

            m_EnemeisController.Init();
            m_MainCharacterController.Init();
            m_LevelController.Init();
            m_MISCController.Init();

            m_LevelController.FirstArena();
            //StartGameLoop();
        }

        private void OnDragJoystickPlayer(Vector2 vector2)
        {
            m_MainCharacterController.Move(vector2);
        }

        private void OnDestroy()
        {
            m_PlayerInput.OnInput -= OnDragJoystickPlayer;
            m_EnemeisController.OnAllDeadEnemies -= m_MainCharacterController.StopShoot;
            m_LevelController.OnSetChunk -= OnSetChunk;
        }

        private void StartEnemyGameLoop()
        {
            m_EnemeisController.StartShoot();
            m_EnemeisController.Attach();
        }

        private void StartMainCharacterGameLoop()
        {
            m_MainCharacterController.StartShoot();
        }

        private void OnAllDeadEnemies()
        {
            PlayerController.Instance.CompleteArena();
            m_MainCharacterController.StopShoot();

            if (PlayerController.Instance.IsCompleteLevel())
            {
                PlayerController.Instance.CompleteLevel();

                SavePlayerData.SaveData();
                EventManager.Win();
            }
            else
            {
                PlayerController.Instance.StartRoad();
                m_LevelController.NextArena();
            }
        }

        private void OnSetChunk(Chunk chunk)
        {
            m_EnemeisController.SetEnemies(chunk.Enemies);
            m_EnemeisController.InitEnemy();
            m_WeaponsController.GaveWeapons(m_EnemeisController.NeedingWeapons);
            m_EnemeisController.InitEnemeisWeapons();

            StartCoroutine(WaitCoroutine());
            StartMainCharacterGameLoop();
        }

        private void OnPlayerDied()
        {
            PlayerController.Instance.LoseLevel();
        }

        private IEnumerator WaitCoroutine()
        {
            yield return StartCoroutine(SpawnPause());
            
            StartEnemyGameLoop();
        }

        private IEnumerator SpawnPause()
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}