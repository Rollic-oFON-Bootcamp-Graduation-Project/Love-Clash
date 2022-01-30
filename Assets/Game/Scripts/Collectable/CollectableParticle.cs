using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableParticle : MonoBehaviour
{
    [SerializeField] public ParticleSystem[] particles;
    public void PlayCollected()
    {
        particles[0].Play();
    }

    public void UpdateParticle(ParticleType type)
    {
        switch (type)
        {
            case ParticleType.LOVE:
                particles[0].Play();
                break;
            case ParticleType.HATE:
                particles[1].Play();
                break;
        }
    }
}
