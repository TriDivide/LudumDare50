using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{

    private GameObject[] spawners;
    public GameObject zombie;
    public float spawnDelayEasy = 5f;
    public float spawnDelayMed = 3f;
    public float spawnDelayHard = 1f;

    public int currentZombieCount = 0;

    private float spawnDelay = 5f;

    private Difficulty difficultyLevel = Difficulty.Easy;
    
    ZombieSpawnerController() {
        _ = new ScoreModel();
    }

    public void startSpawning() {
        this.spawners = GameObject.FindGameObjectsWithTag("Spawner");


        StartCoroutine(Spawning());


    }

    IEnumerator Spawning() {
        while (true) {
            yield return new WaitForSeconds(spawnDelay);
            if (ScoreModel.Instance.score > 30) {
                SpawnSevenZombies();
            }
            if (ScoreModel.Instance.score > 25) {
                SpawnSixZombies();
            }
            if (ScoreModel.Instance.score > 20) {
                SpawnFiveZombies();
            }
            if (ScoreModel.Instance.score > 15) {
                SpawnFourZombies();
            }
            else if (ScoreModel.Instance.score > 10) {
                
                SpawnThreeZombies();
            }
            else if (ScoreModel.Instance.score > 5) {
                SpawnTwoZombies();
            }
            else {
                SpawnZombie();
            }
        }
    }

    private void Update() {
        this.currentZombieCount = GameObject.FindGameObjectsWithTag("Zombie").Length;
        if (ScoreModel.Instance.score > 100) {
            spawnDelay = spawnDelayHard;
        }
        else if (ScoreModel.Instance.score > 50) {
            spawnDelay = spawnDelayMed;
        }
        else {
            spawnDelay = spawnDelayEasy;
        }
    }


    private void SpawnZombie() {
        if (this.currentZombieCount < 50) {
            if (this.spawners.Length > 0) {
                GameObject spawner = this.spawners[Random.Range(0, spawners.Length - 1)];

                Instantiate(zombie, spawner.transform.position, spawner.transform.rotation);
            }
        }
    }

    private void SpawnTwoZombies() {
        if (this.currentZombieCount < 20) {
            if (this.spawners.Length > 0) {


                GameObject spawnerOne = this.spawners[0];
                GameObject spawnerTwo = this.spawners[spawners.Length - 1];

                Instantiate(zombie, spawnerOne.transform.position, spawnerOne.transform.rotation);
                Instantiate(zombie, spawnerTwo.transform.position, spawnerTwo.transform.rotation);

            }
        }
    }

    private void SpawnThreeZombies() {
        GameObject spawnerOne = this.spawners[0];
        GameObject spawnerTwo = this.spawners[1];
        GameObject spawnerThree = this.spawners[spawners.Length - 1];

        Instantiate(zombie, spawnerOne.transform.position, spawnerOne.transform.rotation);
        Instantiate(zombie, spawnerTwo.transform.position, spawnerTwo.transform.rotation);
        Instantiate(zombie, spawnerThree.transform.position, spawnerTwo.transform.rotation);

    }

    private void SpawnFourZombies() {
        GameObject spawnerOne = this.spawners[0];
        GameObject spawnerTwo = this.spawners[1];
        GameObject spawnerThree = this.spawners[2];
        GameObject spawnerFour = this.spawners[3];

        Instantiate(zombie, spawnerOne.transform.position, spawnerOne.transform.rotation);
        Instantiate(zombie, spawnerTwo.transform.position, spawnerTwo.transform.rotation);
        Instantiate(zombie, spawnerThree.transform.position, spawnerThree.transform.rotation);
        Instantiate(zombie, spawnerFour.transform.position, spawnerFour.transform.rotation);

    }
    private void SpawnFiveZombies() {
        GameObject spawnerOne = this.spawners[0];
        GameObject spawnerTwo = this.spawners[1];
        GameObject spawnerThree = this.spawners[2];
        GameObject spawnerFour = this.spawners[3];
        GameObject spawnerFive = this.spawners[4];

        Instantiate(zombie, spawnerOne.transform.position, spawnerOne.transform.rotation);
        Instantiate(zombie, spawnerTwo.transform.position, spawnerTwo.transform.rotation);
        Instantiate(zombie, spawnerThree.transform.position, spawnerThree.transform.rotation);
        Instantiate(zombie, spawnerFour.transform.position, spawnerFour.transform.rotation);
        Instantiate(zombie, spawnerFive.transform.position, spawnerFive.transform.rotation);

    }
    private void SpawnSixZombies() {
        GameObject spawnerOne = this.spawners[0];
        GameObject spawnerTwo = this.spawners[1];
        GameObject spawnerThree = this.spawners[2];
        GameObject spawnerFour = this.spawners[3];
        GameObject spawnerFive = this.spawners[4];
        GameObject spawnerSix = this.spawners[5];

        Instantiate(zombie, spawnerOne.transform.position, spawnerOne.transform.rotation);
        Instantiate(zombie, spawnerTwo.transform.position, spawnerTwo.transform.rotation);
        Instantiate(zombie, spawnerThree.transform.position, spawnerThree.transform.rotation);
        Instantiate(zombie, spawnerFour.transform.position, spawnerFour.transform.rotation);
        Instantiate(zombie, spawnerFive.transform.position, spawnerFive.transform.rotation);
        Instantiate(zombie, spawnerSix.transform.position, spawnerSix.transform.rotation);


    }
    private void SpawnSevenZombies() {
        GameObject spawnerOne = this.spawners[0];
        GameObject spawnerTwo = this.spawners[1];
        GameObject spawnerThree = this.spawners[2];
        GameObject spawnerFour = this.spawners[3];
        GameObject spawnerFive = this.spawners[4];
        GameObject spawnerSix = this.spawners[5];
        GameObject spawnerSeven = this.spawners[6];

        Instantiate(zombie, spawnerOne.transform.position, spawnerOne.transform.rotation);
        Instantiate(zombie, spawnerTwo.transform.position, spawnerTwo.transform.rotation);
        Instantiate(zombie, spawnerThree.transform.position, spawnerThree.transform.rotation);
        Instantiate(zombie, spawnerFour.transform.position, spawnerFour.transform.rotation);
        Instantiate(zombie, spawnerFive.transform.position, spawnerFive.transform.rotation);
        Instantiate(zombie, spawnerSix.transform.position, spawnerSix.transform.rotation);
        Instantiate(zombie, spawnerSeven.transform.position, spawnerSeven.transform.rotation);


    }

    enum Difficulty {
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }

}
