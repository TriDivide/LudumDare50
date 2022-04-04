using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {


    public float movementSpeed = 5f;
    public Rigidbody2D rigidBody; 

    public bool isCarryingEquipment = false;

    public float zombieHealth = 10f;

    [SerializeField]
    private GameObject ammo, equipment;

    // Used for pathfinding
    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    public AudioSource spawnSound, deathSound, pickupSound;






    void Start() {
        SearchForPickup();
    }

    private void SearchForPickup() {
        GameObject[] weights = GameObject.FindGameObjectsWithTag("Equipment");
        if (weights.Length != 0) {
            GameObject weight = weights[Random.Range(0, weights.Length - 1)];
            Vector3 weightPos = weight.transform.position;

            SetTargetPosition(weightPos);
        }
        else {
            SetTargetPosition(new Vector3(5, 6, 0));
        }
    }

    private void Update() {
        HandleMovement();
    }

    private void FixedUpdate() {
        Vector3 position = transform.position;

        if (position.x > Pathfinding.Instance.GetGrid().GetWidth() || position.x < 0) {
            Destroy(gameObject);
        }

        if (position.y > Pathfinding.Instance.GetGrid().GetHeight() || position.y < 0) {
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

        if(isCarryingEquipment) {
            Destroy(gameObject);
        }
        else {
            SearchForPickup();
        }
    }

    private void GoToDespawn() {
        pathVectorList = null;
        SetTargetPosition(new Vector3(0, 0, 0));
    }

    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);

                float speed = isCarryingEquipment ? movementSpeed * 0.7f : movementSpeed;
                
                transform.position = transform.position + moveDir * speed * Time.deltaTime;

               
                float angle = (Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg) - 90f;

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        if (collision.GetType() == typeof(BoxCollider2D) && collision.gameObject.tag == "Equipment") {
            if (!isCarryingEquipment) {
                pickupSound.Play();
                isCarryingEquipment = true;
                Destroy(collision.gameObject);
                GoToDespawn();
            }
        }
        // interacted with a bullet.
        if (collision.GetType() == typeof(BoxCollider2D) && collision.gameObject.tag == "Bullet") {
            if (zombieHealth <= 0) {
                // Zombie is dead. 
                deathSound.Play();

           
                if (isCarryingEquipment && equipment != null) {
                    GameObject _ = Instantiate(equipment, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                }

                int spawnChance = Random.Range(0, 2);
                Debug.Log(spawnChance);
                if (spawnChance > 0) {
                    GameObject _ = Instantiate(ammo, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.Euler(0, 0, 0));
                }
                Destroy(collision.gameObject);
                Destroy(gameObject);
                ScoreModel.Instance.SetScore(1);

            }
            else {
                zombieHealth -= 10;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Zombie") {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.collider.gameObject.GetComponent<BoxCollider2D>());
        }
        if (collision.collider.gameObject.tag == "Wall") {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.collider.gameObject.GetComponent<BoxCollider2D>());
        }
        if (collision.collider.gameObject.tag == "WallContainer") {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.collider.gameObject.GetComponent<CompositeCollider2D>());
        }
        if (collision.collider.gameObject.tag == "Ammo") {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.collider.gameObject.GetComponent<BoxCollider2D>());
        }
    }

}

enum Direction {
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3
}

