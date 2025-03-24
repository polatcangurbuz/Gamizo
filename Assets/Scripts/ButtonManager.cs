using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening; // DOTween k�t�phanesini dahil etmelisin.

public class ButtonManager : MonoBehaviour
{
  
    [SerializeField]  CanvasGroup canvasGroup;
    private Vignette vignette;

    private void Start()
    {
        vignette = PostProcessManager.instance.vignette;
   
    }

    public void StartButton()
    {        
        if (vignette == null) return;

        canvasGroup.DOFade(0f, 1f).OnComplete(() => {
            canvasGroup.gameObject.SetActive(false); // Tamamen g�r�nmez olduktan sonra devre d��� b�rak
        });

        // Vignette yo�unlu�unu 1'den 0'a belirli bir s�re i�inde d���r
        vignette.intensity.value = 1f; // �nlem ama�l� tekrar 1'e ayarl�yoruz.

        DOTween.To(
            () => vignette.intensity.value,   // Ba�lang�� de�eri
            x => vignette.intensity.value = x, // G�ncelleme fonksiyonu
            0f,   // Biti� de�eri
            5f    // S�re (3 saniye)
        ).SetEase(Ease.InOutQuad); // Yumu�ak ge�i� i�in easing fonksiyonu

    }
}
