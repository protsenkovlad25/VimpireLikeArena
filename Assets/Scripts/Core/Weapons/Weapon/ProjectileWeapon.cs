using VampireLike.General;

namespace VampireLike.Core.Weapons
{
    public class ProjectileWeapon : WeaponBehaviour, INeeding<IAttaching>
    {
        private int m_CurrentNumberBullets;

        public override void Init()
        {
            m_Pool = new PoolBehaviour<Projectile>();
            m_Pool.CreateParent(transform);
            m_Pool.Pooling(m_WeaponData.ProjectilePref, 2);
            m_Moving = new ProjectileMovement();
            m_CurrentNumberBullets = m_WeaponData.MagazineSize;
        }

        public override void StopShoot()
        {
            m_Pool.ReturnAllPull();
        }

        //private void OnHit(Projectile projectile)
        //{
        //    m_Pool.ReturnToPool(projectile);
        //}
    }
}