using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingGrid : MonoBehaviour { //Thx @UnityCodeMonkey :)
    private Vector3 gridScale;
    private Tilemap tilemap;

    private BoundsInt bounds;
    private PathNode[,] nodes;

    public PathfindingGrid Test() {
        gridScale = new Vector3(0.32f,0.32f,1f);
        tilemap = GetComponent<Tilemap>();
        GetBounds();
        CreatePathNodes();

        return this;
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
                Vector3 a = GetCellPositionGrid(new Vector3Int(x,y,0));
                Vector3 nodePosition = new Vector3(a.x*0.32f+0.16f,a.y*0.32f+0.16f,a.z);
                nodes[x,y] = new PathNode(x,y,nodePosition);
            }
        }
    }

    private Vector3Int GetCellPositionGrid(Vector3Int tilesetCellPosition) {
        int worldX = tilesetCellPosition.x + bounds.position.x;
        int worldY = tilesetCellPosition.y + bounds.position.y;
        int worldZ = tilesetCellPosition.z + bounds.position.z;

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

    public Vector3Int GetCellPositionTileset(Vector3Int gridCellPosition) {
        int localX = gridCellPosition.x - bounds.position.x;
        int localY = gridCellPosition.y - bounds.position.y;
        int localZ = gridCellPosition.z - bounds.position.z;

        return new Vector3Int(localX, localY, localZ);
    }

    public Vector3Int GetWorldPositionGrid(Vector3 worldPosition) {
        Vector3 resultPosition = worldPosition - new Vector3(gridScale.x/2,gridScale.y/2,gridScale.z/2);
        resultPosition = Vector3.Scale(resultPosition,new Vector3(1/gridScale.x, 1/gridScale.y, 1/gridScale.z));

        return Vector3Int.RoundToInt(resultPosition);
    }
}