using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{

    private GameObject[] spawners;
    public GameObject zombie;
    public float spawnDelay = 10f;
    // Start is called before the first frame update
    void Start() {
        this.spawners = GameObject.FindGameObjectsWithTag("Spawner");

        InvokeRepeating("SpawnZombie", 0f, spawnDelay);

    }


    private void SpawnZombie() {
        if(this.spawners != null) {
            GameObject spawner = this.spawners[Random.Range(0, spawners.Length)];

            Instantiate(zombie, spawner.transform.position, spawner.transform.rotation);
        }
    }



}
