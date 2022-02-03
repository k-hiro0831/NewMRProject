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

    private Vector3 _pos; 

    bool _atk;

    bool _check;

    bool _atkBool;

    private Player _playerSc;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMainModule = particleSystem.main;
        _player = GameObject.FindGameObjectWithTag("MainCamera");
        _playerSc = _player.GetComponent<Player>();
    }

    public void Atk(bool atk)
    {
        _atk = atk;
    }

    private void Post()
    {
        
    }

    void LateUpdate()
    {
        if (_atk && !_check)
        {
            _pos = _player.transform.position;
            _check = true;
        }

        if (!_atk)
        {
            _check = false;
        }

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
                    targetTransformedPosition = transform.InverseTransformPoint(_pos);
                    break;
                }
            case ParticleSystemSimulationSpace.Custom:
                {
                    targetTransformedPosition = particleSystemMainModule.customSimulationSpace.InverseTransformPoint(_pos);
                    break;
                }
            case ParticleSystemSimulationSpace.World:
                {
                    targetTransformedPosition = _pos;
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

            float disA = Vector3.Distance(_pos, particles[i].position);
            float disB = Vector3.Distance(_player.transform.position, _pos);

            if (disA < 0.9f && disB < 0.9f && !_atkBool)
            {
                _atkBool = true;
                _playerSc.Atk();
                Invoke("AtkBoolReset", 2.0f);
            }
        }

        particleSystem.SetParticles(particles, particleCount);

    }

    private void AtkBoolReset()
    {
        _atkBool = false;
    }
}