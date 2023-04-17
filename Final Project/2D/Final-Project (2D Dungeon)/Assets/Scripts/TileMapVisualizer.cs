using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap;

    [SerializeField]
    private TileBase floorTile;

    public void PaintFloorTile(IEnumerable<Vector2Int> floorPosition)
    {
        PaintTiles(floorPosition, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach(var position in positions)
        {
            PaintTile(tileMap, tile, position);
        }
    }

    private void PaintTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);

        tileMap.SetTile(tilePosition, tile);
    }
}
