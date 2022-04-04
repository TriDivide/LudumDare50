using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel {

    public static ScoreModel Instance { get; private set; } = new ScoreModel();


    public int score { get; private set; }
    public bool didDie { get; private set; }
    public ScoreModel() {
        score = 0;
        didDie = false;

    }

    public void SetScore(int updatedScore) {
        score += updatedScore;
    }

    public void hasDied() {
        didDie = true;
    }


}
