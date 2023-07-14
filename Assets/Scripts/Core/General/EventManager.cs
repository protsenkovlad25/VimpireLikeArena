using UnityEngine;
using UnityEngine.Events;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

namespace VampireLike.Core.General
{
    public static class EventManager
    {
        public static UnityEvent OnLose = new UnityEvent();
        public static UnityEvent OnWin = new UnityEvent();
        public static UnityEvent OnStartArena = new UnityEvent();
        public static UnityEvent OnAllWavesSpawned = new UnityEvent();
        public static UnityEvent MainCharacterTakeDamage = new UnityEvent();
        public static UnityEvent<EnemyCharacter, IMoving> OnSwitchMovement = new UnityEvent<EnemyCharacter, IMoving>();
        public static UnityEvent<EnemyCharacter, ILooking> OnSwitchLook = new UnityEvent<EnemyCharacter, ILooking>();
        public static UnityEvent<INeedingWeapon, GameObject> OnSwitchWeapon = new UnityEvent<INeedingWeapon, GameObject>();
        public static UnityEvent<INeedingWeapon, GameObject> OnWeaponReceived = new UnityEvent<INeedingWeapon, GameObject>();

        public static void Lose()
        {
            OnLose?.Invoke();
        }

        public static void Win()
        {
            OnWin?.Invoke();
        }

        public static void StartArena()
        {
            OnStartArena?.Invoke();
        }

        public static void AllWavesSpawned()
        {
            OnAllWavesSpawned?.Invoke();
        }

        public static void TakingDamage()
        {
            MainCharacterTakeDamage?.Invoke();
        }

        public static void SwitchMovement(EnemyCharacter enemyCharacter, IMoving moving)
        {
            OnSwitchMovement?.Invoke(enemyCharacter, moving);
        }

        public static void SwitchLook(EnemyCharacter enemyCharacter, ILooking looking)
        {
            OnSwitchLook?.Invoke(enemyCharacter, looking);
        }

        public static void SwitchWeapon(INeedingWeapon needingWeapon, GameObject weaponPrefab)
        {
            OnSwitchWeapon?.Invoke(needingWeapon, weaponPrefab);
        }

        public static void WeaponReceived(INeedingWeapon needingWeapon, GameObject weaponPrefab)
        {
            OnWeaponReceived?.Invoke(needingWeapon, weaponPrefab);
        }
    }
}
