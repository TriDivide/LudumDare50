using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    private int width;
    private int height;
    private float cellSize;

    private int[,] gridArray;
    public Grid(int width, int height, float cellSize) {

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        Debug.Log(width + " " + height);

        for(int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                Debug.Log(x + " , " + y);
                GenerateGridSquareText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize)  * .5f, 20);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize;
    }

    private TextMesh GenerateGridSquareText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40) {
        GameObject t = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = t.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = t.GetComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Left;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = Color.black;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = 5000;
        return textMesh;
    }
}
