using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    [SerializeField] private PostProcessVolume processVolume;

    [HideInInspector]
    public Vignette vignette;
    [HideInInspector]
    public ChromaticAberration ChromaticAberration;

    public static PostProcessManager instance;

    private void Awake()
    {
        instance = this;

        // Vignette bileþenini alýyoruz
        if (processVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 1f; // Baþlangýçta tam karanlýk
        }
        if (processVolume.profile.TryGetSettings(out ChromaticAberration))
        {
            ChromaticAberration.intensity.value = 0f;
        }
    }



}
