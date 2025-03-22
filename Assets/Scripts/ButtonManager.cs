using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening; // DOTween kütüphanesini dahil etmelisin.

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PostProcessVolume processVolume;
    private Vignette vignette;

    private void Start()
    {
        // Vignette bileþenini alýyoruz
        if (processVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 1f; // Baþlangýçta tam karanlýk
        }
    }

    public void StartButton()
    {
        if (vignette == null) return;

        // Vignette yoðunluðunu 1'den 0'a belirli bir süre içinde düþür
        vignette.intensity.value = 1f; // Önlem amaçlý tekrar 1'e ayarlýyoruz.
        DOTween.To(
            () => vignette.intensity.value,   // Baþlangýç deðeri
            x => vignette.intensity.value = x, // Güncelleme fonksiyonu
            0f,   // Bitiþ deðeri
            5f    // Süre (3 saniye)
        ).SetEase(Ease.InOutQuad); // Yumuþak geçiþ için easing fonksiyonu
    }
}
