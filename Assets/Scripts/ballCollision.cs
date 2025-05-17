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

                Debug.Log("Karakterin caný: " + characterHealth.Instance.Health);
            }
        }
    }

    private void setChromaticAberration()
    {
        ChromaticAberration.intensity.value = 1f;
        DOTween.To(
         () => ChromaticAberration.intensity.value,   // Baþlangýç deðeri
         x => ChromaticAberration.intensity.value = x, // Güncelleme fonksiyonu
         0f,   // Bitiþ deðeri
         2f    // Süre (2 saniye)
         ).SetEase(Ease.InOutQuad); // Yumuþak geçiþ için easing fonksiyonu

    }



}
