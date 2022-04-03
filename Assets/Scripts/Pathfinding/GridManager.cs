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

    public GameObject spawner;

    private bool hasSpawned = false;

    void Start() {

        pathfinding = new Pathfinding(gridSizeX, gridSizeY, cellSize);


        // Spawn Spawners
        if (spawnX > -1 && spawnY > -1) {
            Grid<PathNode> grid = pathfinding.GetGrid();
            PathNode[,] cells = grid.GetAllGridObjects();
            float size = grid.GetCellSize();
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


    }

    void Update() {
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


    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }



}
