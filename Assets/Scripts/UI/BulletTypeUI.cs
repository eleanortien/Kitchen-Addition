using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTypeUI : MonoBehaviour
{
    public Button button;
    public ShooterController shooter;
    public bool isSushi;
    public bool isPizza;

    private void Update()
    {
        if (shooter.bulletType == "sushi" && isSushi)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = new Color32(255, 255, 255, 255);
            button.colors = colors;
        }
        else if (isSushi)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = new Color32(0, 0, 0, 255);
            button.colors = colors;
        }
        if (shooter.bulletType == "pizza" && isPizza)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = new Color32(255, 255, 255, 255);
            button.colors = colors;
        }
        else if (isPizza)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = new Color32(0, 0, 0, 255);
            button.colors = colors;
        }
    }
}
