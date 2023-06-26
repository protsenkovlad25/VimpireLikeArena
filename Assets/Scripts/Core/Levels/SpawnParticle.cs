using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_SpawnParticle;

    public void PlayParticle()
    {
        m_SpawnParticle.Play();
    }
}
