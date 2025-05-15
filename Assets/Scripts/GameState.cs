using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    bool isGameOver;
    [SerializeField] GameObject gameOverCanvas;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(characterHealth.Instance.Health <= 0)
        {
            isGameOver = true;
        }
        if(isGameOver)
        {
            Time.timeScale = 0f;
            GameObject.Find("AudioManager").GetComponent<AudioSource>().mute = true;
            gameOverCanvas.SetActive(true);
        }

    }
}
