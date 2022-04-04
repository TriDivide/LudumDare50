using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {

    public int ammoValue = 0;

    public AudioSource reload;
    public void OnDestroy() {
        reload.Play();
    }
}
