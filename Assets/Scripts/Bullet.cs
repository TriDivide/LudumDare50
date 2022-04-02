using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {



    // Start is called before the first frame update
    void Start() {

        Invoke("Despawn", 5f);
    }

    
    void Despawn() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag != "Weapon") {
            Destroy(gameObject);
        }
    }
}
