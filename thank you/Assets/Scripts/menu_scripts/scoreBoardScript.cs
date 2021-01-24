using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreBoardScript : MonoBehaviour
{
    private Transform entryConrainer;
    private Transform scoreTemplate;

    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    void Awake()
    {
        entryConrainer = transform.Find("ScoreContainer");
        scoreTemplate = entryConrainer.Find("ScoreTemplate");

        scoreTemplate.gameObject.SetActive(false);

        /*
        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { score = 521854 , name = "BBB"},
            new HighscoreEntry { score = 456127 , name = "AAA"},
            new HighscoreEntry { score = 461397 , name = "JON"},
            new HighscoreEntry { score = 125678 , name = "KOT"},
            new HighscoreEntry { score = 485915 , name = "CAT"},
        };
        */


        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        

        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for(int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
                
            }
        }

        highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryConrainer, highscoreEntryTransformList);
        }

        /*
        HighScores highscores = new HighScores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highScoreTable"));
        */
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry , Transform container , List <Transform> tranformList)  
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(scoreTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * tranformList.Count);

        entryTransform.gameObject.SetActive(true);

        int rank = tranformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;


        tranformList.Add(entryTransform);
    }
    

    private class HighScores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }


    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}

