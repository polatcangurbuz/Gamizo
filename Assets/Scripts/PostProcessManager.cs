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

        // Vignette bile�enini al�yoruz
        if (processVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 1f; // Ba�lang��ta tam karanl�k
        }
        if (processVolume.profile.TryGetSettings(out ChromaticAberration))
        {
            ChromaticAberration.intensity.value = 0f;
        }
    }



}
