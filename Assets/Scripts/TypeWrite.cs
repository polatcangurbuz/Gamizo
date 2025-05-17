using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWrite : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // TMP UI objesi
    public float typingSpeed = 0.05f;

    //[TextArea(3, 10)]
    //public string fullText = "Merhaba, bu bir typewriter efektidir!";
    private string fullText;
    void Start()
    {
        if (textDisplay != null)
        {
            fullText = textDisplay.text;    
            StartCoroutine(TypeText());
        }
        else
            Debug.LogError("Text Display atanmadý! Inspector'da TextMeshProUGUI bileþeni sürüklenmeli.");
    }

    IEnumerator TypeText()
    {
        textDisplay.text = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            textDisplay.text += fullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
