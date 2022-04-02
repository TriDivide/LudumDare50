using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bullet;
    public Transform firepoint;


    public void Fire() {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
    }

}
