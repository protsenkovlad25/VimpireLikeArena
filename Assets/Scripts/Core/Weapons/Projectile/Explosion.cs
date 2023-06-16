using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float m_CurrentExpDistance;
    [SerializeField] private float m_RepulsiveForce;
    [SerializeField] private ParticleSystem m_SmokeParticle;
    [SerializeField] private ParticleSystem m_SparksParticle;

    public float CurrentExpDistance => m_CurrentExpDistance;
    public float RepulsiveForce => m_RepulsiveForce;

    public void PlayParticleExplosion()
    {
        m_SmokeParticle.Play();
        m_SparksParticle.Play();
    }
}
