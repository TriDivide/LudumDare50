using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour {

    public Text healthText;
    public double playerHealth = 1;

    public bool underAttack = false;

    public double damage = 0.6;

    void Start()
    {
        
    }

    void FixedUpdate() {
        healthText.text = "Health: " + playerHealth.ToString("0");

        if (underAttack) {
            playerHealth -= damage;
        }

        if (playerHealth <= 0) {

            ScoreModel.Instance.hasDied();
            Debug.Log(ScoreModel.Instance.didDie);

            SceneManager.LoadScene(sceneName: "GameOver");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.GetType() == typeof(BoxCollider2D) && collision.collider.gameObject.tag == "Zombie") {
            underAttack = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.GetType() == typeof(BoxCollider2D) && collision.collider.gameObject.tag == "Zombie") {
            underAttack = false;
        }
    }
}
