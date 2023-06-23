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
            //m_EnemeisController.OnAllDeadEnemies += OnAllDeadEnemies;
            m_EnemeisController.OnAllDeadEnemies += m_LevelController.IsCompleteWavesCluster;
            m_LevelController.OnSetChunk += OnSetChunk;
            m_LevelController.OnAllWavesSpawned += LevelCompleteCheck;

            EventManager.OnLose.AddListener(OnPlayerDied);
            EventManager.OnSwitchWeapon.AddListener(SwitchEnemyWeapon);

            m_EnemeisController.Init();
            m_MainCharacterController.Init();
            m_LevelController.Init();
            m_MISCController.Init();

            m_LevelController.FirstArena();
            //StartGameLoop();
        }

        private void SwitchEnemyWeapon(WeaponType weaponType, INeedingWeapon needingWeapon)
        {
            m_WeaponsController.GaveWeapon(weaponType, needingWeapon);
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

        private void LevelCompleteCheck()
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
                m_MISCController.ChangeCameraLimit();
            }
        }

        private void OnSetChunk(Chunk chunk)
        {
            StartCoroutine(InitEnemies(chunk));
        }

        private IEnumerator InitEnemies(Chunk chunk)
        {
            if (m_LevelController.IsFight == false)
            {
                m_MISCController.ChangeCameraLimit();
            }
            m_EnemeisController.SetEnemies(chunk.Enemies);
            m_EnemeisController.InitEnemy(chunk.Enemies);
            m_EnemeisController.SetMark(chunk.Enemies);

            //m_MainCharacterController.StopShoot();

            yield return new WaitForSeconds(1f);

            m_EnemeisController.ActivateEnemies(chunk.Enemies);
            m_WeaponsController.GaveWeapons(m_EnemeisController.NeedingWeapons);
            m_EnemeisController.InitEnemeisWeapons();
            m_EnemeisController.Landing(chunk.Enemies);

            //m_EnemeisController.RemoveMarks();

            m_LevelController.PauseSpawn = false;

            m_MainCharacterController.StopShoot();
            StartCoroutine(WaitCoroutine());
            StartCoroutine(StartMainCharacterGameLoop());
        }

        private void OnPlayerDied()
        {
            PlayerController.Instance.LoseLevel();
        }

        private IEnumerator StartMainCharacterGameLoop()
        {
            yield return new WaitForSeconds(.5f);
            m_MainCharacterController.StartShoot();
        }

        private IEnumerator WaitCoroutine()
        {
            yield return StartCoroutine(SpawnPause());
            
            StartEnemyGameLoop();
        }

        private IEnumerator SpawnPause()
        {
            yield return new WaitForSeconds(1.3f);
        }
    }
}