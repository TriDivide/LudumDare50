using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour {


    private Transform entryContainer, entryTemplate;

    private void Awake() {
        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");


        entryTemplate.gameObject.SetActive(false);


        for (int i = 0; i < 10; i++) {

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

    // Single highscore entry
    private class HighscoreEntry {
        public int score;
        public string name;
    }
}
