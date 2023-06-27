using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_DeathParticle;

    public void PlayParticle()
    {
        m_DeathParticle.Play();
    }
}
