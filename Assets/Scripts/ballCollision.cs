using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ballCollision : MonoBehaviour
{


    private ChromaticAberration ChromaticAberration;
   
    private void Start()
    {
        ChromaticAberration = PostProcessManager.instance.ChromaticAberration;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (characterHealth.Instance != null)
            {
               
                if(TypeWrite.Instance.isStoryFinished)
                characterHealth.Instance.Health -= 10;

                ParticleSystemManager.Instance.onParticleElectricityEffect();

                setChromaticAberration();

                Debug.Log("Karakterin can�: " + characterHealth.Instance.Health);
            }
        }
    }

    private void setChromaticAberration()
    {
        ChromaticAberration.intensity.value = 1f;
        DOTween.To(
         () => ChromaticAberration.intensity.value,   // Ba�lang�� de�eri
         x => ChromaticAberration.intensity.value = x, // G�ncelleme fonksiyonu
         0f,   // Biti� de�eri
         2f    // S�re (2 saniye)
         ).SetEase(Ease.InOutQuad); // Yumu�ak ge�i� i�in easing fonksiyonu

    }



}
