using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Looked at how to video
public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;

    [SerializeField]
    private Tilemap wallTilemap;

    [SerializeField]
    private TileBase floorTile;

    [SerializeField]
    private TileBase wallTop;

    [SerializeField]
    private TileBase wallSideRight;

    [SerializeField]
    private TileBase wallSideLeft;

    [SerializeField]
    private TileBase wallBottom;

    [SerializeField]
    private TileBase wallFull;

    [SerializeField]
    private TileBase wallInnerCornerDownLeft;

    [SerializeField]
    private TileBase wallInnerCornerDownRight;

    [SerializeField]
    private TileBase wallDiagonalCornerDownRight;

    [SerializeField]
    private TileBase wallDiagonalCornerDownLeft;

    [SerializeField]
    private TileBase wallDiagonalCornerUpRight;

    [SerializeField]
    private TileBase wallDiagonalCornerUpLeft;

    public void PaintFloorTile(IEnumerable<Vector2Int> floorPosition)
    {
        PaintTiles(floorPosition, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions)
        {
            PaintTile(tilemap, tile, position);
        }
    }

    public void PaintWall(Vector2Int position, string binaryType)
    {
        int typeInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypes.wallTop.Contains(typeInt))
        {
            tile = wallTop;
        }

        else if(WallTypes.wallSideRight.Contains(typeInt))
        {
            tile = wallSideRight;
        }

        else if (WallTypes.wallSideLeft.Contains(typeInt))
        {
            tile = wallSideLeft;
        }

        else if (WallTypes.wallBottom.Contains(typeInt))
        {
            tile = wallBottom;
        }

        else if (WallTypes.wallFull.Contains(typeInt))
        {
            tile = wallFull;
        }

        if (tile != null)
        {
            PaintTile(wallTilemap, tile, position);
        }

        
    }

    public void PaintCornerWall(Vector2Int position, string binaryType)
    {
        int typeInt = Convert.ToInt32(binaryType, 2);

        TileBase tile = null;

        if (WallTypes.wallInnerCornerDownLeft.Contains(typeInt))
        {
            tile = wallInnerCornerDownLeft;
        }

        else if (WallTypes.wallInnerCornerDownRight.Contains(typeInt))
        {
            tile = wallInnerCornerDownRight;
        }

        else if (WallTypes.wallDiagonalCornerDownLeft.Contains(typeInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }

        else if (WallTypes.wallDiagonalCornerDownRight.Contains(typeInt))
        {
            tile = wallDiagonalCornerDownRight;
        }

        else if (WallTypes.wallDiagonalCornerUpRight.Contains(typeInt))
        {
            tile = wallDiagonalCornerUpRight;
        }

        else if (WallTypes.wallDiagonalCornerUpLeft.Contains(typeInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }

        else if (WallTypes.wallFullEightDirections.Contains(typeInt))
        {
            tile = wallFull;
        }

        else if (WallTypes.wallBottomEightDirections.Contains(typeInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
        {
            PaintTile(wallTilemap, tile, position);
        }
    }

    private void PaintTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);

        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
