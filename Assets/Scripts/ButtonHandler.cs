using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class ButtonHandler : MonoBehaviour {

    [SerializeField]
    private InputField nameField;

    public void RestartGame() {
        Save();
        SceneManager.LoadScene(sceneName: "Gameplay");
    }

    public void StartGame() {
        SceneManager.LoadScene(sceneName: "Gameplay");
    }

    public void QuitGame() {
        Save();
        Application.Quit();
    }

    public void GoHome() {
        SceneManager.LoadScene(sceneName: "StartScene");
    }

    public void GoToHighscore() {
        SceneManager.LoadScene(sceneName: "HighScoreScene");
    }

    private void Save() {
        if (nameField != null) {
            int score = ScoreModel.Instance.score;
            string name = nameField.text;

            AddHighscoreEntry(score, name);
        }
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
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }
}
