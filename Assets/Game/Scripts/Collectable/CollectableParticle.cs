using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableParticle : MonoBehaviour
{
    [SerializeField] public ParticleSystem[] particles;
    [SerializeField] private ParticleSystem defaultParticle;
    
    public void UpdateParticle(ParticleType type)
    {
        switch (type)
        {
            case ParticleType.LOVE:
                StopParticle(defaultParticle);
                PlayParticle(particles[(int)type]);
                break;
            case ParticleType.HATE:
                PlayParticle(particles[(int)type]);
                break;
        }
    }

    public void StopParticle(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
        particle.Stop(true);
    }
    public void PlayParticle(ParticleSystem particle)
    {
        particle.Play();
    }
}
