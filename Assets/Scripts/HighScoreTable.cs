using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighScoreTable : MonoBehaviour {


    private Transform entryContainer, entryTemplate;

    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");


        entryTemplate.gameObject.SetActive(false);


        // read scores from prefs.
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores != null && highscores.highscoreEntryList != null) {
            List<HighscoreEntry> highScoreList = highscores.highscoreEntryList;

            highscoreEntryTransformList = new List<Transform>();

            foreach (HighscoreEntry highscoreEntry in highScoreList) {
                CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            }
        }
    }

    private void CreateHighScoreEntryTransform(HighscoreEntry highScoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 20f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();        
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;

        string rankString;
        switch (rank) {
            default:
                rankString = rank + "TH"; break;
            case 1:
                rankString = "1ST"; break;
            case 2:
                rankString = "2ND"; break;
            case 3:
                rankString = "3RD"; break;
        }


        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        entryTransform.Find("NameText").GetComponent<Text>().text = highScoreEntry.name;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = highScoreEntry.score.ToString();

        transformList.Add(entryTransform);
    } 

    private void AddHighscoreEntry(int score, string name) {
        // Check if we have data from the prefs. If we don't then set something so that it isn't a null pointer
        if (JsonUtility.FromJson<Highscores>(PlayerPrefs.GetString("highScoreTable")) == null) {
            List<HighscoreEntry> d = new List<HighscoreEntry> { new HighscoreEntry { score = -1, name = "" } };
            Highscores defaultHighScores = new Highscores { highscoreEntryList = d };
            string defaultValue = JsonUtility.ToJson(defaultHighScores);

            PlayerPrefs.SetString("highScoreTable", defaultValue);
            PlayerPrefs.Save();

        }
        // Create new entry.
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Get high score
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // If we contain the default entry as the first item then we remove it so not to pollute the json.
        if (highscores.highscoreEntryList[0].score == -1) {
            highscores.highscoreEntryList.RemoveAt(0);
        }

        // add new entry to high score
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Make sure the high score is in order
        highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));
        
        // limit the high score to the ten highest scores (means you dont need to scroll through potentially hundreds in the UI.)
        List<HighscoreEntry> limitedItems = highscores.highscoreEntryList.Take(10).ToList();
        highscores.highscoreEntryList = limitedItems;

        // Save the updated list.
        string json = JsonUtility.ToJson(highscores);
        Debug.Log(json);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

}

public class Highscores {
    public List<HighscoreEntry> highscoreEntryList;
}

// Single highscore entry
[System.Serializable]
public class HighscoreEntry {
    public int score;
    public string name;
}
