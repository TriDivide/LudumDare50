using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    public void RestartGame() {
        SceneManager.LoadScene(sceneName: "SampleScene");
    }

    public void StartGame() {
        SceneManager.LoadScene(sceneName: "LevelGeneratorTest");
    }

}
