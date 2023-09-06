using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITesting : MonoBehaviour
{
    [SerializeField] private UIInputWindow inputWindow;
    public Image startMessage;

    private void Start()
    {
    
         
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputWindow.Show("Enter Name", "Type Here");
            startMessage.enabled = false;
        }
    }
   
}
