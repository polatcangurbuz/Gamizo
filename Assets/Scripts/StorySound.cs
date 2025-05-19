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
        audioSource.clip = storyClip[index];
        audioSource.Play();
    }



}
