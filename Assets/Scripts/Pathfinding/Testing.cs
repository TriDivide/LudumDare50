using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    private Pathfinding pathfinding;

    void Start() {

        pathfinding = new Pathfinding(10, 10);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i = 0; i < path.Count - 1; i++) {
                    PathNode node = path[i];
                    PathNode node2 = path[i + 1];
                    Debug.Log(node);
                    Debug.Log(node2);
                    Debug.DrawLine(new Vector3(node.x, node.y) * 10f + Vector3.one * 5f, new Vector3(node2.x, node2.y) * 10f + Vector3.one * 5f, Color.green);
                }
            }
        }
    }


    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }



}
