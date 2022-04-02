using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {


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
        ammoCount = 10;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        print("interacted with object");
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ammo") {
            GameObject ammo = collision.gameObject;
            addAmmo(ammo.GetComponent<AmmoManager>().ammoValue);
            Destroy(ammo);
        }
    }

    public void addAmmo(int amount) {
        ammoCount += amount;
        print(ammoCount);
    }

    public void removeAmmo(int amount) {
        ammoCount -= amount;
        print(ammoCount);
    }


}
