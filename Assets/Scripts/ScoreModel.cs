using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel {

    public static ScoreModel Instance { get; private set; }


    public int score { get; private set; }

    public ScoreModel() {
        Instance = this;

        score = 0;
    }

    public void SetScore(int updatedScore) {
        score += updatedScore;
    }


}
