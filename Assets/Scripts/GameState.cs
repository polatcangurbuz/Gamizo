using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameWinCanvas;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float countdown = 120f;
    private bool isPlaying = false;

    private void Update()
    {
        if (gameCanvas.activeSelf && !storyPanel.activeSelf)
        {
            isPlaying = true;
        }
        else if (gameCanvas.activeSelf && music.isPlaying)
        {
            music.Stop();
        }

        if (isPlaying)
        {
            if (!music.isPlaying)
            {
                music.Play();
            }

            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, 120f);

            int minutes = (int)(countdown / 60f);
            int seconds = (int)(countdown % 60f);

            countdownText.text = countdown > 60 ?
                string.Format("{0}:{1:00}", minutes, seconds) :
                seconds.ToString();
        }

        if (characterHealth.Instance != null && characterHealth.Instance.Health <= 0)
        {
            Time.timeScale = 0f;
            if (audioSource != null) audioSource.mute = true;
            gameOverCanvas.SetActive(true);
        }

        if (countdown <= 0)
        {
            Time.timeScale = 0f;
            gameWinCanvas.SetActive(true);
        }
    }
}