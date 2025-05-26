using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    [SerializeField] private string gasURL;
    private string prompt;
    string response;
    [SerializeField] TMP_InputField inputFieldusers;
    [SerializeField] TMP_InputField inputFieldgemini;
    bool controlButton = false;
    private void Update()
    {
        if (controlButton)
        {
            StartCoroutine(SendDataToGAS());
            controlButton = false;
        }
    }

    private IEnumerator SendDataToGAS()
    {
        prompt = inputFieldusers.text;
        WWWForm form = new WWWForm();
        form.AddField("parameter", prompt);
        UnityWebRequest www = UnityWebRequest.Post(gasURL, form);

        yield return www.SendWebRequest();
        response = "";

        if (www.result == UnityWebRequest.Result.Success)
        {
            response = www.downloadHandler.text;
        }
        else
        {
            response = "Hata";
        }

        inputFieldgemini.text = response;
        Debug.Log(response);
    }

    public void promptInput(string value)
    {
        prompt = value;
    }

    public void butonControl()
    {
        controlButton = true;
    }



}