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


        float templateHeight = 20f;
        for (int i = 0; i < 10; i++) {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;

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

            int score = Random.Range(0, 1000);
            string name = "AAA";

            entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
            entryTransform.Find("NameText").GetComponent<Text>().text = name;    
            entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        }
    }
}
