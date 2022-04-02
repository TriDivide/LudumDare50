using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera sceneCamera;
    public Rigidbody2D rigidBody;
    public Weapon weapon;
    public float movementSpeed = 0f;

    public GameObject projectileOrigin;
    public WeaponManager weaponManager;

    private Vector2 mousePosition;
    private Vector2 moveDirection;

    



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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetMouseButtonDown(0)) {
            if (weaponManager.ammoCount > 0) {
                weapon.Fire();
                weaponManager.removeAmmo(1);
            }
        }

    }

    void Move() {
        
        //Moving the player.
        rigidBody.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);

        // Aiming the weapon
        Vector2 aimDirection = mousePosition - rigidBody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
       
        if (projectileOrigin != null) {
            projectileOrigin.GetComponent<Rigidbody2D>().rotation = aimAngle;
            projectileOrigin.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);


        }


    }
}
