using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject chatCanvas;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Vignette vignette;

    private void Start()
    {
        Time.timeScale = 0f;
        if (vignette == null && PostProcessManager.instance != null)
        {
            vignette = PostProcessManager.instance.vignette;
        }
    }

    public void StartButton()
    {
        if (vignette == null) return;

        canvasGroup.DOFade(0f, 1f).OnComplete(() =>
        {
            canvasGroup.gameObject.SetActive(false);
        });

        vignette.intensity.value = 1f;
        DOTween.To(() => vignette.intensity.value,
                   x => vignette.intensity.value = x,
                   0f, 5f).SetEase(Ease.InOutQuad);

        Time.timeScale = 1f;
        gameCanvas.SetActive(true);
    }

    public void OpenOptions() => optionsCanvas.SetActive(true);
    public void CloseOptions() => optionsCanvas.SetActive(false);

    public void AdjustMusicVolume()
    {
        if (audioSource != null && musicSlider != null)
        {
            audioSource.volume = musicSlider.value;
        }
    }

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void LowButton() => QualitySettings.SetQualityLevel(0);
    public void MediumButton() => QualitySettings.SetQualityLevel(1);
    public void HighButton() => QualitySettings.SetQualityLevel(2);

    public void OpenChat() { chatCanvas.SetActive(true); }
    public void CloseChat() { chatCanvas.SetActive(false); }
}