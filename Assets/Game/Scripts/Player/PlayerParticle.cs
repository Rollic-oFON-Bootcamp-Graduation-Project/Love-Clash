using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] downgradeParticles;
    [SerializeField] private ParticleSystem[] upgradeParticles;

    private void OnEnable()
    {
        Observer.PlayerUpdate += PlayParticle;
    }
    private void OnDestroy()
    {
        Observer.PlayerUpdate -= PlayParticle;
    }
    private void PlayParticle(int value)
    {
        var particles = (value > 0) ? upgradeParticles : downgradeParticles;
        particles[0].Play();
        particles[1].Play();
    }
}
