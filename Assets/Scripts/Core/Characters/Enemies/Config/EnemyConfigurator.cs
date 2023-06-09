using UnityEngine;

namespace VampireLike.Core.Characters
{
    public class EnemyConfigurator : MonoBehaviour
    {
        [SerializeField] private EnemyConfig m_EnemyConfig;

        public CharacterData GetData(EnemyType enemyType)
        {
            var data = m_EnemyConfig.EnemyData.Find(enemy => enemy.EnemyType.Equals(enemyType));

            var enemyData = new CharacterData()
            {
                HealthPoints = data.HealthPoints,
                Speed = data.Speed,
                BaseDamage = data.BaseDamage
            };

            return enemyData;
        }

        public IMoving GetMovement(EnemyType enemyType)
        {
            var data = m_EnemyConfig.EnemyData.Find(enemy => enemy.EnemyType.Equals(enemyType));

            return data.EnemyMovementType switch
            {
                EnemyMovementType.DirectedMovement => new DirectedMovement(),
                EnemyMovementType.DashMovement => new DashMovement(),
                EnemyMovementType.DistanceMovement => new DirectedMovement(),
                _ => new DirectedMovement(),
            };
        }

        public ILooking GetLooking(EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.SpikyEnemy => new SimpleLook(),
                EnemyType.PushingEnemy => new SimpleLook(),
                EnemyType.ShootingEnemy => new CheckLook(),
                EnemyType.RocketShootingEnemy => new CheckLook(),
                _ => new SimpleLook(),
            };
        }
    }
}
