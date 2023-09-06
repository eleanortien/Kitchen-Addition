using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIButton : MonoBehaviour
{
    public string sceneName;
    public bool isRestart;
    public bool isStart;
    public GameObject rulesUI;
    public AudioClip pressButtonSound;
    public ShooterController shooter;
    public string bulletType;

    private void Start()
    {
        if (isRestart == true && SceneTracker.instance.lastScene == "Start")
        {
            gameObject.SetActive(false);
        }
        else if (isRestart)
        {
            gameObject.SetActive(true);
        }
     
    }

    public void Click()
    {
        AudioSource.PlayClipAtPoint(pressButtonSound, Camera.main.transform.position);
        SceneTracker.instance.SceneChange(sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ChangeBulletType()
    {
        shooter.ChangeType(bulletType);
    }

}
