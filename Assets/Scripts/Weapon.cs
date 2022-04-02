using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bullet;
    public Transform firepoint;

    public float fireForce;


    public void Fire() {
        Vector3 origin = firepoint.position;
        GameObject projectile = Instantiate(bullet, origin, firepoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firepoint.up * fireForce, ForceMode2D.Impulse);
    }

}
