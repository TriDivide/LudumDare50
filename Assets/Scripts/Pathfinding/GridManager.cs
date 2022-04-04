using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    private Pathfinding pathfinding;

    [SerializeField]
    private int gridSizeX = 10, gridSizeY = 10;

    [SerializeField]
    private float cellSize = 1f;

    [SerializeField]
    private GameObject spawner, wall, weight, wallContainer;

    private Coords[] weightGeneratePositions = {
                    new Coords(11,3), new Coords(12,3), new Coords(14,3), new Coords(15,3), new Coords(9,5), 
                    new Coords(9,6), new Coords(15,6), new Coords(15,7), new Coords(3,8), new Coords(3,9), 
                    new Coords(3,10), new Coords(3,11), new Coords(15,12), new Coords(9,13), new Coords(15,13), 
                    new Coords(9,14), new Coords(11,16), new Coords(12,16), new Coords(14,16), new Coords(15,16)
    };

    private Coords[] spawnerGeneratePositions = { new Coords(8, 1), new Coords(2, 2), new Coords(18, 2), new Coords(18, 9), new Coords(3, 17), new Coords(18, 17), new Coords(8, 18) };


    private Coords[] wallGeneratePositions = {
        new Coords(10,2), new Coords(11,2), new Coords(12,2), new Coords(13,2), new Coords(14,2), 
        new Coords(15,2), new Coords(16,2), new Coords(10,3), new Coords(13,3), new Coords(16,3), 
        new Coords(2,4), new Coords(3,4), new Coords(4,4), new Coords(5,4), new Coords(6,4), 
        new Coords(7,4), new Coords(8,4), new Coords(9,4), new Coords(10,4), new Coords(13,4), 
        new Coords(16,4), new Coords(10,5), new Coords(16,5), new Coords(10,6), new Coords(16,6), 
        new Coords(2,7), new Coords(3,7), new Coords(4,7), new Coords(5,7), new Coords(6,7), 
        new Coords(10,7), new Coords(16,7), new Coords(2,8), new Coords(5,8), new Coords(6,8), 
        new Coords(10,8), new Coords(11,8), new Coords(12,8), new Coords(16,8), new Coords(2,9), 
        new Coords(16,9), new Coords(2,10), new Coords(16,10), new Coords(2,11), new Coords(5,11), 
        new Coords(6,11), new Coords(10,11), new Coords(11,11), new Coords(12,11), new Coords(16,11), 
        new Coords(2,12), new Coords(3,12), new Coords(4,12), new Coords(5,12), new Coords(6,12), 
        new Coords(10,12), new Coords(16,12), new Coords(10,13), new Coords(16,13), new Coords(10,14), 
        new Coords(16,14), new Coords(2,15), new Coords(3,15), new Coords(4,15), new Coords(5,15), 
        new Coords(6,15), new Coords(7,15), new Coords(8,15), new Coords(9,15), new Coords(10,15), 
        new Coords(13,15), new Coords(16,15), new Coords(10,16), new Coords(13,16), new Coords(16,16), 
        new Coords(10,17), new Coords(11,17), new Coords(12,17), new Coords(13,17), new Coords(14,17), 
        new Coords(15,17), new Coords(16,17)
    };

    private Coords playerPos = new Coords(8, 10);
    private bool isTesting = false;
    
    void Start() {

        

        pathfinding = new Pathfinding(gridSizeX, gridSizeY, cellSize);
        Grid<PathNode> grid = pathfinding.GetGrid();
        float size = grid.GetCellSize();

        // Generate the weights in the right place.
        foreach(Coords pos in weightGeneratePositions) {
            Instantiate(weight, new Vector3((pos.X * size) + (size / 2), (pos.Y * size) + (size / 2), 0), Quaternion.Euler(0, 0, 0));
        }

        // Once the weights have been generated then enable the equipmentManager to keep track of them.
        gameObject.GetComponent<GymEquipmentManager>().enabled = true;

        // Generate the walls
        foreach (Coords pos in wallGeneratePositions) {
            GameObject wallObj = Instantiate(wall, new Vector3((pos.X * size) + (size / 2), (pos.Y * size) + (size / 2), 0), Quaternion.Euler(0, 0, 0));
            
            wallObj.transform.parent = wallContainer.transform;
            //Make sure ai cant go through walls.
            pathfinding.GetGrid().GetXY(new Vector3((pos.X * size), (pos.Y * size), 0), out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(false);
        }

        // Generate Spawners
        foreach(Coords pos in spawnerGeneratePositions) {
            Instantiate(spawner, new Vector3((pos.X * size) + (size / 2), (pos.Y * size) + (size / 2), 0), Quaternion.Euler(0, 0, 0));
        }

        //Position the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3((playerPos.X * size) + (size / 2), (playerPos.Y * size) + (size / 2), -10);

        //find the spawn manager and begin the spawning now we have generated spawners.
        GameObject spawnManager = GameObject.FindGameObjectWithTag("SpawnManager");
        ZombieSpawnerController spawnController = spawnManager.GetComponent(typeof(ZombieSpawnerController)) as ZombieSpawnerController;
        if (spawnController != null) { spawnController.startSpawning(); }
    }   

    void Update() {
        if (isTesting) {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
                List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
                if (path != null) {
                    for (int i = 0; i < path.Count - 1; i++) {
                        Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1)) {
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
                pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            }
        }
    }


    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

}

public struct Coords {
    public int X { get; }
    public int Y { get; }
    public Coords(int x, int y) {
        X = x;
        Y = y;
    }



}
