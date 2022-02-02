using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HomingParticles : MonoBehaviour
{
    public Transform target;
    public float force = 3.0f;

    new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    ParticleSystem.MainModule particleSystemMainModule;

    private GameObject _player;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMainModule = particleSystem.main;
        _player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void LateUpdate()
    {
        int maxParticles = particleSystemMainModule.maxParticles;

        if (particles == null || particles.Length < maxParticles)
        {
            particles = new ParticleSystem.Particle[maxParticles];
        }

        particleSystem.GetParticles(particles);
        float forceDeltaTime = force * Time.deltaTime;

        Vector3 targetTransformedPosition;

        switch (particleSystemMainModule.simulationSpace)
        {
            case ParticleSystemSimulationSpace.Local:
                {
                    targetTransformedPosition = transform.InverseTransformPoint(_player.transform.position);
                    break;
                }
            case ParticleSystemSimulationSpace.Custom:
                {
                    targetTransformedPosition = particleSystemMainModule.customSimulationSpace.InverseTransformPoint(_player.transform.position);
                    break;
                }
            case ParticleSystemSimulationSpace.World:
                {
                    targetTransformedPosition = _player.transform.position;
                    break;
                }
            default:
                {
                    throw new System.NotSupportedException(

                        string.Format("Unsupported simulation space '{0}'.",
                        System.Enum.GetName(typeof(ParticleSystemSimulationSpace), particleSystemMainModule.simulationSpace)));
                }
        }

        int particleCount = particleSystem.particleCount;

        for (int i = 0; i < particleCount; i++)
        {
            Vector3 directionToTarget = Vector3.Normalize(targetTransformedPosition - particles[i].position);
            Vector3 seekForce = directionToTarget * forceDeltaTime;
            //if (particles[i].remainingLifetime > (particles[i].startLifetime - 0.5f))
            particles[i].velocity += seekForce;
        }

        particleSystem.SetParticles(particles, particleCount);
    }
}