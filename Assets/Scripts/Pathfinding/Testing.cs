using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    private Grid grid;

    void Start() {
        grid = new Grid(4, 2, 10f);        
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            grid.SetValue(GetMouseWorldPosition(), Random.Range(0, 10));
        }

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log(grid.GetValue(GetMouseWorldPosition()));
        }
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }



}
