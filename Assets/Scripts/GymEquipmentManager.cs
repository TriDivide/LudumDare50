using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GymEquipmentManager: MonoBehaviour {

    private int totalEquipmentCount = 6;

    private bool hasLoaded = false;
    void Start() {

        totalEquipmentCount = GameObject.FindGameObjectsWithTag("Equipment").Length;

        hasLoaded = true;

    }


    private void FixedUpdate() {

        if (hasLoaded) {
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

            int zombieCount = 0;
            foreach (GameObject zombie in zombies) {
                if (zombie.GetComponent<ZombieController>().isCarryingEquipment) {
                    zombieCount += 1;
                }
            }

            int currentEquipmentCount = GameObject.FindGameObjectsWithTag("Equipment").Length;


            if (zombieCount + currentEquipmentCount == 0) {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
