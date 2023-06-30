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
        [SerializeField] private SolidObjectsController m_SolidObjectsController;
        [SerializeField] private PlayerInput m_PlayerInput;
        [SerializeField] private MainCharacterController m_MainCharacterController;
        [SerializeField] private WeaponsController m_WeaponsController;
        [SerializeField] private MISCController m_MISCController;
        [SerializeField] private Level m_Level;

        public void ControllersInit()
        {
            m_EnemeisController.SetAttaching(m_MainCharacterController);
            m_MainCharacterController.SetAttaching(m_EnemeisController);
            m_WeaponsController.GaveWeapon(m_MainCharacterController);

            m_PlayerInput.OnInput += OnDragJoystickPlayer;
            m_EnemeisController.OnAllDeadEnemies += NextWave;
            m_Level.OnSetChunk += OnSetChunk;
            m_Level.OnSpawnPauseEnd += Activates;
            m_Level.OnArenaIsCleared += LevelCompleteCheck;

            EventManager.OnLose.AddListener(OnPlayerDied);
            EventManager.OnSwitchWeapon.AddListener(SwitchEnemyWeapon);

            m_EnemeisController.Init();
            m_SolidObjectsController.Init();
            m_MainCharacterController.Init();
            m_MISCController.Init();
            m_Level.Init();

            m_MISCController.ChangeCameraLimit();
            m_Level.FirstArena();
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
            m_Level.OnSetChunk -= OnSetChunk;
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
                m_Level.NextArena();
                m_MISCController.ChangeCameraLimit();
            }
        }

        private void OnSetChunk(Chunk chunk)
        {
            // -- Ememies -- //
            m_EnemeisController.SetEnemies(chunk.Enemies);
            m_EnemeisController.InitEnemy(chunk.Enemies);
            m_EnemeisController.SetMark(chunk.Enemies);

            // -- Walls -- //
            m_SolidObjectsController.SetWalls(chunk.Walls);
            m_SolidObjectsController.InitWalls();
            m_SolidObjectsController.SetMark();
        }

        private void Activates(Chunk chunk)
        {
            // -- Enemies -- //
            m_EnemeisController.ActivateEnemies(chunk.Enemies);
            m_WeaponsController.GaveWeapons(m_EnemeisController.NeedingWeapons);
            m_EnemeisController.InitEnemeisWeapons();
            m_EnemeisController.Landing(chunk.Enemies);

            // -- Walls -- //
            m_SolidObjectsController.Landing();

            // -- StartShoot Enemies and MainCharacter -- //
            m_MainCharacterController.StopShoot();
            StartCoroutine(WaitCoroutine());
            StartCoroutine(StartMainCharacterGameLoop());
        }

        private void NextWave()
        {
            m_Level.NextWave();
            m_MainCharacterController.StopShoot();
        }

        private void OnPlayerDied()
        {
            PlayerController.Instance.LoseLevel();
        }

        private IEnumerator StartMainCharacterGameLoop()
        {
            yield return new WaitForSeconds(.8f);
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