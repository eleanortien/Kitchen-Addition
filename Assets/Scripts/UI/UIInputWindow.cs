using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIInputWindow : MonoBehaviour
{
    public Button confirmButton;
    public Button cancelButton;
    public TextMeshProUGUI titleText;
    public TMP_InputField inputField;
    private string inputText;
    public int characterLimit = 5;
    private bool firstLogin = true;


    private void Awake()
    {
   
        inputField.characterLimit = characterLimit;
        Hide();
        
    }
    private void Start()
    {
        firstLogin = true;
    }
    public void Show(string titleString, string inputString)
    {
        if (firstLogin == true)
        {
            gameObject.SetActive(true);
            titleText.text = titleString;
            inputField.text = inputString;
            firstLogin = false;
        }
        else
        {
          SceneManager.LoadScene("MainLevel");
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void ConfirmInput()
    {
        inputText = inputField.text;
        PlayerPrefs.SetString("PlayerName", inputText);
        SceneManager.LoadScene("HighscoreBoard");
        Hide();
    }
    public void CancelInput()
    {
        inputField.text = "";
        Hide();
    }
}
