using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {

    private int totalEquipmentCount = 1;

    void Start() {
        totalEquipmentCount = GameObject.FindGameObjectsWithTag("Equipment").Length;
    }


    private void FixedUpdate() {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

        int zombieCount = 0;
        foreach(GameObject zombie in zombies) {
            if (zombie.GetComponent<ZombieController>().isCarryingEquipment) {
                zombieCount += 1;
            }
        }

        int currentEquipmentCount = GameObject.FindGameObjectsWithTag("Equipment").Length;

        print(totalEquipmentCount);
        print(zombieCount + currentEquipmentCount);

        if (totalEquipmentCount > zombieCount + currentEquipmentCount) {
            SceneManager.LoadScene("GameOver");
        }

    }
}
