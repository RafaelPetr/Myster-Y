using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode { //Thx @UnityCodeMonkey :)
    private int x;
    private int y;
    private Vector3 worldPosition;

    private int gCost; //Cost for movement since start
    private int hCost; //Distance to goal
    private int fCost; //hCost + gCost

    private PathNode lastNode;

    public PathNode(int x, int y, Vector3 worldPosition) {
        this.x = x;
        this.y = y;
        this.worldPosition = worldPosition;
    }

    #region Get and Set functions
    public int GetX() {
        return x;
    }

    public int GetY() {
        return y;
    }

    public Vector3 GetWorldPosition() {
        return worldPosition;
    }

    public int GetGCost() {
        return gCost;
    }

    public void SetGCost(int value) {
        gCost = value;
    }

    public int GetHCost() {
        return hCost;
    }

    public void SetHCost(int value) {
        hCost = value;
    }

    public int GetFCost() {
        return fCost;
    }

    public void SetFCost() {
        fCost = gCost + hCost;
    }

    public PathNode GetLastNode() {
        return lastNode;
    }

    public void SetLastNode(PathNode pathNode) {
        lastNode = pathNode;
    }
    #endregion
}