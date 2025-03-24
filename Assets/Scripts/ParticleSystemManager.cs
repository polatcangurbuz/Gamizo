using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{

    [SerializeField] GameObject electricityEffect;
    ParticleSystem _particleSystem;

    public static ParticleSystemManager Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
        _particleSystem = electricityEffect.GetComponent<ParticleSystem>();
    }



    public void onParticleElectricityEffect()
    {
        if (_particleSystem == null) return;
            _particleSystem.Play();
    }

}
