using UnityEngine.Events;
using VampireLike.Core;
using VampireLike.Core.Characters.Enemies;
using VampireLike.Core.Weapons;

public static class EventManager
{
    public static UnityEvent OnLose = new UnityEvent();
    public static UnityEvent OnWin = new UnityEvent();
    public static UnityEvent MainCharacterTakeDamage = new UnityEvent();
    public static UnityEvent<EnemyCharacter, IMoving> OnSwitchMovement = new UnityEvent<EnemyCharacter, IMoving>();
    public static UnityEvent<WeaponType, INeedingWeapon> OnSwitchWeapon = new UnityEvent<WeaponType, INeedingWeapon>();

    public static void Lose()
    {
        OnLose?.Invoke();
    }

    public static void Win()
    {
        OnWin?.Invoke();
    }

    public static void TakingDamage()
    {
        MainCharacterTakeDamage?.Invoke();
    }

    public static void SwitchMovement(EnemyCharacter enemyCharacter, IMoving moving)
    {
        OnSwitchMovement?.Invoke(enemyCharacter, moving);
    }

    public static void SwitchWeapon(WeaponType weaponType, INeedingWeapon needingWeapon)
    {
        OnSwitchWeapon?.Invoke(weaponType, needingWeapon);
    }
}
