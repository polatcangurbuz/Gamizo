using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening; // DOTween kütüphanesini dahil etmelisin.

public class ButtonManager : MonoBehaviour
{
  
    [SerializeField]  CanvasGroup canvasGroup;
    [SerializeField] GameObject gameCanvas;
    private Vignette vignette;


    private void Start()
    {
        Time.timeScale = 0;
        vignette = PostProcessManager.instance.vignette;
   
    }

    public void StartButton()
    {        
        if (vignette == null) return;

        canvasGroup.DOFade(0f, 1f).OnComplete(() => {
            canvasGroup.gameObject.SetActive(false); // Tamamen görünmez olduktan sonra devre dýþý býrak
        });

        // Vignette yoðunluðunu 1'den 0'a belirli bir süre içinde düþür
        vignette.intensity.value = 1f; // Önlem amaçlý tekrar 1'e ayarlýyoruz.

        DOTween.To(
            () => vignette.intensity.value,   // Baþlangýç deðeri
            x => vignette.intensity.value = x, // Güncelleme fonksiyonu
            0f,   // Bitiþ deðeri
            5f    // Süre (3 saniye)
        ).SetEase(Ease.InOutQuad); // Yumuþak geçiþ için easing fonksiyonu
        Time.timeScale = 1f;
        gameCanvas.SetActive(true);
    }
}
