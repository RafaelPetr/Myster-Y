using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingGrid : MonoBehaviour { //Thx @UnityCodeMonkey :)
    public static PathfindingGrid instance;
    private Tilemap tilemap;

    private BoundsInt bounds;
    private PathNode[,] nodes;

    private void Awake() {
        if (this != null) {
            instance = this;
        }

        tilemap = GetComponent<Tilemap>();
        GetBounds();
        CreatePathNodes();
    }

    private void GetBounds() {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
    }

    private void CreatePathNodes() {
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        nodes = new PathNode[bounds.size.x,bounds.size.y];

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null) {
                    continue;
                }
                
                Vector3 nodePosition = tilemap.GetCellCenterWorld(GetWorldCellPosition(new Vector3Int(x,y,0)));
                nodes[x,y] = new PathNode(x,y,nodePosition);
            }
        }
    }

    private Vector3Int GetWorldCellPosition(Vector3Int localCellPosition) {
        int worldX = localCellPosition.x + bounds.position.x;
        int worldY = localCellPosition.y + bounds.position.y;
        int worldZ = localCellPosition.z - bounds.position.z;

        return new Vector3Int(worldX, worldY, worldZ);
    }

    public int GetWidth() {
        return bounds.size.x;
    }

    public int GetHeight() {
        return bounds.size.y;
    }

    public PathNode GetNode(int x, int y) {
        return nodes[x,y];
    }

    public Vector3Int GetLocalCellPosition(Vector3Int worldCellPosition) {
        int localX = worldCellPosition.x - bounds.position.x;
        int localY = worldCellPosition.y - bounds.position.y;
        int localZ = worldCellPosition.z - bounds.position.z;

        return new Vector3Int(localX, localY, localZ);
    }
}

//Cool functions:
//Debug.Log(tilemap.cellBounds);
//Debug.Log(tilemap.HasTile(new Vector3Int(-12,4,0)));
//Debug.Log(tilemap.layoutGrid.GetCellCenterWorld(new Vector3Int(-18,4,0)));
//character.position = tilemap.layoutGrid.GetCellCenterWorld(new Vector3Int(-18,4,0));