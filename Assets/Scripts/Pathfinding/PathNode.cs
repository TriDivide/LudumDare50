using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathNode : MonoBehaviour {
    
    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public Guid uuid;

    public bool isWalkable;
    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;

        isWalkable = true;

        this.uuid = System.Guid.NewGuid();
    }

    public override string ToString() {
        return x + "," + y;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

}