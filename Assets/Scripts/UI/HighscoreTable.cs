using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public float templateHeight = 20f;
   

    
    private List<Transform> highscoreEntryTransformList;
    public static HighscoreTable instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        
        entryTemplate.gameObject.SetActive(false);

        
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
    
        
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        int rank = transformList.Count + 1;
        string rankstring;
        switch (rank)
        {
            default:
                rankstring = rank + "TH"; break;
            case 1: rankstring = "1ST"; break;
            case 2: rankstring = "2ND"; break;
            case 3: rankstring = "3RD"; break;
        }
        entryTransform.Find("posText").GetComponent<Text>().text = rankstring;
        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;
        entryTransform.Find("EntryBackground").gameObject.SetActive(rank % 2 == 1);

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Sort + Limit
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry temporary = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temporary;
                }
            }
        }
        int n = 10;
        if (highscores.highscoreEntryList.Count >= n)
        {
            highscores.highscoreEntryList.RemoveAt(n - 1);
        }

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighscoreTable", json);
        PlayerPrefs.Save();

    }
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Single Entry
    [System.Serializable]
   private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
