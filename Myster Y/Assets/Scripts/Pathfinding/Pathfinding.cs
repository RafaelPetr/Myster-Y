using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding { //Thx @UnityCodeMonkey :)
    private int straightCost = 10;

    private PathfindingGrid grid;
    private List<PathNode> openList;
    private HashSet<PathNode> closedList;

    public Pathfinding(PathfindingGrid testGrid) {
        grid = testGrid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        openList = new List<PathNode>();
        closedList = new HashSet<PathNode>();

        PathNode startNode = grid.GetNode(startX,startY);
        PathNode endNode = grid.GetNode(endX,endY);

        openList.Add(startNode);

        for (int x = 0; x < grid.GetWidth(); x++) {
            for (int y = 0; y < grid.GetHeight(); y++) {
                PathNode currentNode = grid.GetNode(x,y);
                if (currentNode == null) {
                    continue;
                }
                currentNode.SetGCost(int.MaxValue);
                currentNode.SetFCost();
                currentNode.SetLastNode(null);
            }
        }

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistanceCost(startNode, endNode));
        startNode.SetFCost();

        while (openList.Count > 0) {
            PathNode currentNode = GetLowestFCostNode(openList);

            if (currentNode == endNode) {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) {
                if (closedList.Contains(neighbourNode)) {
                    continue;
                }
                else if (neighbourNode == null) {
                    continue;
                }

                int tentativeGCost = currentNode.GetGCost() + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.GetGCost()) {
                    neighbourNode.SetLastNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistanceCost(neighbourNode, endNode));
                    neighbourNode.SetFCost();

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b) {
        int xDistance = Mathf.Abs(a.GetX() - b.GetX());
        int yDistance = Mathf.Abs(a.GetY() - b.GetY());
        int remaining = Mathf.Abs(xDistance - yDistance);

        return straightCost * remaining;
    }

    private PathNode GetLowestFCostNode(List <PathNode> nodeList) {
        PathNode lowestFCostNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++) {
            if (nodeList[i].GetFCost() < lowestFCostNode.GetFCost()) {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private List<PathNode> CalculatePath(PathNode endNode) {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;
        path.Add(currentNode);

        while (currentNode.GetLastNode() != null) {
            path.Add(currentNode.GetLastNode());
            currentNode = currentNode.GetLastNode();
        }

        path.Reverse();
        
        return path;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode) {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.GetX() - 1 >= 0) { //Left
            neighbourList.Add(grid.GetNode(currentNode.GetX() - 1, currentNode.GetY()));
        }

        if (currentNode.GetX() + 1 < grid.GetWidth()) { //Right
            neighbourList.Add(grid.GetNode(currentNode.GetX() + 1, currentNode.GetY()));
        }

        if (currentNode.GetY() - 1 >= 0) { //Down
            neighbourList.Add(grid.GetNode(currentNode.GetX(), currentNode.GetY() - 1));
        }

        if (currentNode.GetY() + 1 < grid.GetHeight()) { //Up
            neighbourList.Add(grid.GetNode(currentNode.GetX(), currentNode.GetY() + 1));
        }

        return neighbourList;
    }

    public PathfindingGrid GetGrid() {
        return grid;
    }
}
