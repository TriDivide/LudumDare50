using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    public Text scoreText, totalScoreText;

    private ScoreModel model;
    ScoreManager() {
        model = new ScoreModel();

        ScoreModel.Instance.SetScore(0);
    }

    // Start is called before the first frame update
    void Start() {
        if (scoreText != null) {
            scoreText.text = "Current Score: " + ScoreModel.Instance.score.ToString();
        }

        if (totalScoreText != null) {
            totalScoreText.text = "You fended off: " + ScoreModel.Instance.score.ToString() + " Zombros. Well done.";
        }
    }

    // Update is called once per frame
    void Update() {
        if (scoreText != null) {
            scoreText.text = "Current Score: " + ScoreModel.Instance.score.ToString();
        }
    }
}
