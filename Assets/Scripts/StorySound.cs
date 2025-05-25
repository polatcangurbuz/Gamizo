using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySound : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> storyClip;

    [SerializeField]
    AudioSource audioSource;

    public void MusicPlayFunction(int index)
    {
        // Index kontrolü
        if (index < 0 || index >= storyClip.Count || storyClip == null || storyClip.Count == 0)
        {
            Debug.LogWarning("Geçersiz ses indexi veya ses listesi boş!");
            return;
        }

        // Ses çalma işlemi
        audioSource.clip = storyClip[index];
        audioSource.Play();
    }
}
