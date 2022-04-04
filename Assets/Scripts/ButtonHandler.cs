using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    public void RestartGame() {
        SceneManager.LoadScene(sceneName: "Gameplay");
    }

    public void StartGame() {
        SceneManager.LoadScene(sceneName: "Gameplay");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void GoHome() {
        SceneManager.LoadScene(sceneName: "StartScene");
    }

}
