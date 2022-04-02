using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera sceneCamera;
    public Rigidbody2D rigidBody;

    private Vector2 mousePosition;




    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        ProcessInputs();
    }


    void FixedUpdate() {
        Move();    
    }

    void ProcessInputs() {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

    }

    void Move() {
        Vector2 aimDirection = mousePosition - rigidBody.position;

        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = aimAngle;


    }
}
