using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {


    public float movementSpeed = 5f;
    public Rigidbody2D rigidBody; 

    public bool isCarryingEquipment = false;
    public GameObject equipment;

    public float zombieHealth = 10f;

    // Used for pathfinding
    private int currentPathIndex;
    private List<Vector3> pathVectorList;



    void Start() {

        SetTargetPosition(new Vector3(0, 0, 0));
    }

    private void Update() {
        HandleMovement();
    }

    private void FixedUpdate() {
        Vector3 position = transform.position;

        if (position.x > Pathfinding.Instance.GetGrid().GetWidth() || position.x < 1) {
            Destroy(gameObject);
        }

        if (position.y > Pathfinding.Instance.GetGrid().GetHeight() || position.y < 1) {
            Destroy(gameObject);
        }
    }


    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * movementSpeed * Time.deltaTime;
            } 
            else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            } 
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        // tapped a pickup 
        if (collision.GetType() == typeof(CircleCollider2D) && collision.gameObject.tag == "Equipment") {
            if (!isCarryingEquipment) {
                isCarryingEquipment = true;
                Destroy(collision.gameObject);
            }
        }
        // interacted with a bullet.
        if (collision.GetType() == typeof(BoxCollider2D) && collision.gameObject.tag == "Bullet") {
            if (zombieHealth < 0) {
                // Zombie is dead. 
                if (isCarryingEquipment && equipment != null) {
                    GameObject _ = Instantiate(equipment, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                }
                Destroy(collision.gameObject);
                Destroy(gameObject);
                ScoreModel.Instance.SetScore(1);
            }
            else {
                zombieHealth -= 6;
                Destroy(collision.gameObject);
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

