using UnityEngine;

public class MockParticleSystemManager : ParticleSystemManager
{
    private void Awake()
    {
        Instance = this;
        // Don't try to access electricityEffect in tests
    }

    public override void onParticleElectricityEffect()
    {
        // Do nothing in tests
    }
}
