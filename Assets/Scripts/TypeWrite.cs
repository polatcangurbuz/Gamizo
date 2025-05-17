using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWrite : MonoBehaviour
{
    //public TextMeshProUGUI textDisplay; // TMP UI objesi
    public float typingSpeed = 0.05f;

    //[TextArea(3, 10)]
    //public string fullText = "Merhaba, bu bir typewriter efektidir!";
    private string fullText;

    // [SerializeField] List<TextMeshProUGUI> StoryTextList;
    private TextMeshProUGUI tempTMP;
    [SerializeField] List<GameObject> StoryList;
    StorySound StorySound;

    public bool isStoryFinished;

    public static TypeWrite Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StorySound = GetComponent<StorySound>();
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isStoryFinished = false;
        
        for (int j = 0; j < StoryList.Count; j++)
        {
            StoryList[j].SetActive(true);
            StorySound.MusicPlayFunction(j);
            tempTMP = StoryList[j].GetComponent<TextMeshProUGUI>();
            /*textDisplay.text = "*/
            fullText = tempTMP.text;
            StoryList[j].GetComponent<TextMeshProUGUI>().text = "";
            for (int i = 0; i < fullText.Length; i++)
            {
                StoryList[j].GetComponent<TextMeshProUGUI>().text += fullText[i];
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(7f);
            StoryList[j].SetActive(false);
        }
        this.gameObject.SetActive(false);
        isStoryFinished=true;
    }

}
