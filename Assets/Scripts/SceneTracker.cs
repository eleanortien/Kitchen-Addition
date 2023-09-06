using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.root.gameObject);
    }

    
    private string currentScene = "Start";
    public string lastScene = "Start";
    public AudioSource audioSource;
  

    private void Start()
    {
        
    }
    public void SceneChange(string newScene)
        {
            lastScene = currentScene;
            currentScene = newScene;
       
            SceneManager.LoadScene(newScene);
        }
    }
