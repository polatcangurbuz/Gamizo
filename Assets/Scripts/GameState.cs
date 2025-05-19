using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject gameWinCanvas;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject storyPanel;
    [SerializeField] AudioSource music;


    [SerializeField] float countdown = 120f;

    bool isPlaying;

    void Start()
    {
        
    }

    void Update()
    {
        if (gameCanvas.activeSelf && storyPanel.activeSelf == false)
            isPlaying = true;
        else if (gameCanvas.activeSelf && music.isPlaying)
        {
            music.Stop();
        }


        if (isPlaying)
        {
            if(music.isPlaying == false)
            {
                music.Play();
            }

            int temp;

            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, 120f);

            temp = (int)(countdown % 60f);

            if (countdown > 60)
            {
                if (temp >= 10)
                {
                    countdownText.text = ((int)(countdown / 60f)).ToString() + ":" + temp.ToString();
                }
                else
                {
                    countdownText.text = ((int)(countdown / 60f)).ToString() + ":0" + temp.ToString();
                }
            }
            else
            {
                countdownText.text = temp.ToString();
            }
        }



        if (characterHealth.Instance.Health <= 0)
        {
            Time.timeScale = 0f;
            GameObject.Find("AudioManager").GetComponent<AudioSource>().mute = true;
            gameOverCanvas.SetActive(true);
        }
        if(countdown <= 0)
        {
            Time.timeScale = 0f;
            gameWinCanvas.SetActive(true);
        }

    }



}
