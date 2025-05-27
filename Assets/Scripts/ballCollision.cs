using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ballCollision : MonoBehaviour
{
    private ChromaticAberration chromaticAberration;

    private void Start()
    {
        if (PostProcessManager.instance != null)
        {
            chromaticAberration = PostProcessManager.instance.ChromaticAberration;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (characterHealth.Instance != null && (TypeWrite.Instance?.isStoryFinished ?? false))
            {
                characterHealth.Instance.Health -= 10;
                ParticleSystemManager.Instance?.onParticleElectricityEffect();
                TriggerChromaticEffect();
            }
        }
    }

    private void TriggerChromaticEffect()
    {
        if (chromaticAberration == null) return;

        chromaticAberration.intensity.value = 1f;
        DOTween.To(() => chromaticAberration.intensity.value,
                   x => chromaticAberration.intensity.value = x,
                   0f, 2f).SetEase(Ease.InOutQuad);
    }
}