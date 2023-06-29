using UnityEngine;
using VampireLike.Core.Looks;
using VampireLike.Core.Movements;

namespace VampireLike.Core.Characters.Enemies.Config
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
                ScaleDamage = data.ScaleDamage
            };

            return enemyData;
        }

        public IMoving GetMovement(EnemyType enemyType)
        {
            var data = m_EnemyConfig.EnemyData.Find(enemy => enemy.EnemyType.Equals(enemyType));

            IMoving movement = null;
            switch (data.EnemyMovementType)
            {
                case EnemyMovementType.DirectedMovement:
                    movement = new DirectedMovement();
                    break;
                case EnemyMovementType.DashMovement:
                    movement = new DashMovement();
                    break;
                case EnemyMovementType.DistanceMovement:
                    movement = new DistanceMovement();
                    break;
            }

            if (movement == null) movement = new DirectedMovement();

            return movement;
        }

        public ILooking GetLooking(EnemyType enemyType)
        {
            ILooking look = null;
            switch (enemyType)
            {
                case EnemyType.SpikyEnemy:
                    look = new SimpleLook();
                    break;
                case EnemyType.PushingEnemy:
                    look = new SimpleLook();
                    break;
                case EnemyType.ShootingEnemy:
                    look = new CheckLook();
                    break;
                case EnemyType.RocketShootingEnemy:
                    look = new CheckLook();
                    break;
            }

            if (look == null) look = new SimpleLook();

            return look;
        }
    }
}
