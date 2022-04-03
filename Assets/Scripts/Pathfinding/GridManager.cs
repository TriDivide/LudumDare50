using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    private Pathfinding pathfinding;

    [SerializeField]
    private int spawnX = -1, spawnY = -1;

    [SerializeField]
    private int gridSizeX = 10, gridSizeY = 10;

    [SerializeField]
    private float cellSize = 1f;

    [SerializeField]
    private GameObject spawner, wall, weight, wallContainer, player;

    private Coords[] weightGeneratePositions = { new Coords(3,3), new Coords(3,4), new Coords(3,5), new Coords(4,8), new Coords(5,8), new Coords(6,8) };
    
    private Coords[] wallGeneratePositions = { new Coords(2,2), new Coords(3,2), new Coords(5,2), new Coords(6,2), 
                                                new Coords(2,3), new Coords(6,3),new Coords(7,3),
                                                new Coords(2,4), new Coords(7,4),
                                                new Coords(2,5), new Coords(7,5),
                                                new Coords(2,6), new Coords(3,6),
                                                new Coords(3,7), new Coords(7,7),
                                                new Coords(3,8), new Coords(7,8),
                                                new Coords(3,9), new Coords(4,9), new Coords(5,9), new Coords(6,9), new Coords(7,9)};

    private bool hasSpawned = false;

    private bool isTesting = true;
    
    void Start() {

        pathfinding = new Pathfinding(gridSizeX, gridSizeY, cellSize);
        Grid<PathNode> grid = pathfinding.GetGrid();
        float size = grid.GetCellSize();


        // Spawn Spawners
        if (spawnX > -1 && spawnY > -1) {
            PathNode[,] cells = grid.GetAllGridObjects();
            for (int x = 0; x < cells.GetLength(0); x++) {
                for (int y = 0; y < cells.GetLength(1); y++) {
                    if (x == spawnX && y == spawnY) {
                        GameObject _ = Instantiate(spawner, new Vector3(x * size, y * size, 0), Quaternion.Euler(0, 0, 0));
                        hasSpawned = true;
                        break;
                    }
                }
                if (hasSpawned) {
                    break;
                }
            }

        }

        
        // Generate the weights in the right place.
        foreach(Coords pos in weightGeneratePositions) {
            GameObject _ = Instantiate(weight, new Vector3((pos.X * size) + (size / 2), (pos.Y * size) + (size / 2), 0), Quaternion.Euler(0, 0, 0));
        }

        // Generate the walls
        foreach (Coords pos in wallGeneratePositions) {
            GameObject wallObj = Instantiate(wall, new Vector3((pos.X * size) + (size / 2), (pos.Y * size) + (size / 2), 0), Quaternion.Euler(0, 0, 0));
            
            wallObj.transform.parent = wallContainer.transform;
            //Make sure ai cant go through walls.
            pathfinding.GetGrid().GetXY(new Vector3((pos.X * size), (pos.Y * size), 0), out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(false);
        }


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
