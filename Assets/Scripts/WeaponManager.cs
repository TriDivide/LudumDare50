using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {


    public AudioSource reloadSound;


    public int ammoCount { 
        get { 
            return _ammoCount; 
        } 
        private set { 
            _ammoCount = value; 
            ammoText.text = "Remaining Ammo: " + _ammoCount.ToString("0"); 
        } 
    }

    private int _ammoCount;

    public Text ammoText;

    private void Start() {
        ammoCount = 20;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ammo") {
            GameObject ammo = collision.gameObject;
            addAmmo(ammo.GetComponent<AmmoManager>().ammoValue);
            reloadSound.Play();
            Destroy(ammo);
        }
    }

    public void addAmmo(int amount) {
        ammoCount += amount;
    }

    public void removeAmmo(int amount) {
        ammoCount -= amount;
    }


}
