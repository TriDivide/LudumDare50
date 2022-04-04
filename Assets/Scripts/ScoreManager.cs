using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    public Text scoreText, totalScoreText, flavourText;

    ScoreManager() {

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

        if (flavourText != null) {
            Debug.Log(ScoreModel.Instance.didDie);
            if (ScoreModel.Instance.didDie) {
                flavourText.text = "You died in the defence of your gym equipment, your name shall be remembered for all time by those seeking to build a world without zombros...";
            }
            else {
                flavourText.text = "You let the zombros get away with your gym equipment. Now no one can exercise in the apocalypse in peace.";
            }
        }

    }

    // Update is called once per frame
    void Update() {
        if (scoreText != null) {
            scoreText.text = "Current Score: " + ScoreModel.Instance.score.ToString();
        }
    }
}
