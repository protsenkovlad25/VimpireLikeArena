using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float m_CurrentExpDistance = 0.7f;
    [SerializeField] private ParticleSystem m_ParticleSystem1;
    [SerializeField] private ParticleSystem m_ParticleSystem2;

    public float CurrentExpDistance => m_CurrentExpDistance;

    public void PlayParticleExplosion()
    {
        m_ParticleSystem1.Play();
        m_ParticleSystem2.Play();
    }
}
