using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    private bool startMovement = true;
    private bool doMove = true;
    private Vector2 moveDirection;
    private Direction lastMovementDirection;
    public float movementSpeed = 5f;
    public float movingDuration = 0.5f;
    public Rigidbody2D rigidBody; 
    public float stationaryDuration = 1f;

    public bool isCarryingEquipment = false;

    public GameObject equipment;



    void Start() {
        Move();    
    }

    private void Move() {
        if (startMovement) {
            startMovement = false;
            StartCoroutine(Movement());
        }
    }

    IEnumerator Movement() {
        while (1 > 0) {
            float elapsedTime = 0f;

            int movement = Random.Range(0, 3);
            Direction lmd = (Direction)movement;

            while (lmd == lastMovementDirection) {
                int m = Random.Range(0, 3);
                lmd = (Direction)m;
                yield return null;
            }

            lastMovementDirection = lmd;

            if (doMove) {
                switch (lastMovementDirection) {
                    case Direction.Left: { moveDirection = new Vector2(-1, 0).normalized; break; }
                    case Direction.Right: { moveDirection = new Vector2(1, 0).normalized; break; }
                    case Direction.Up: { moveDirection = new Vector2(0, 1).normalized; break; }
                    case Direction.Down: { moveDirection = new Vector2(0, -1).normalized; break; }
                }

                while (elapsedTime < movingDuration) {
                    
                    
                    rigidBody.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                doMove = false;

            }
            else {

                while (elapsedTime < stationaryDuration) {
                    rigidBody.velocity = new Vector2(0, 0);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                doMove = true;
            }


            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.GetType() == typeof(BoxCollider2D) && collision.collider.gameObject.tag == "Bullet") {
            if(isCarryingEquipment && equipment != null) {
                Instantiate(equipment, this.gameObject.transform);
            }
            Destroy(gameObject);
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isCarryingEquipment) {
            if (collision.GetType() == typeof(CircleCollider2D) && collision.gameObject.tag == "Equipment") {
                this.equipment = collision.gameObject;
                Destroy(collision.gameObject);
                isCarryingEquipment = true;
            }
        }
    }

}

enum Direction {
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3
}

