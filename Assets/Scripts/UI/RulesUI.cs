using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesUI : MonoBehaviour
{
    public GameObject rulesUI;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneTracker.instance.lastScene == "Start")
        {
         rulesUI.SetActive(true);
        }
        else
        {
            rulesUI.SetActive(false);
        }
        
    }
}
