using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    public Text scoreText;

    private ScoreModel model;
    ScoreManager() {
        model = new ScoreModel();

        ScoreModel.Instance.SetScore(0);
    }

    // Start is called before the first frame update
    void Start() {
        scoreText.text = "Current Score: " + ScoreModel.Instance.score.ToString();
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = "Current Score: " + ScoreModel.Instance.score.ToString();

    }
}
