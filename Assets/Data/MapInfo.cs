using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Policy;

public class MapInfo
{
    int columns;
    int rows;

    TileInfo[,] mapData;

    // Example tile types and tile data. This may vary depending on the type of game and desired tilemap.
    enum TileType
    {
        Dark,
        Floor,
        Wall,
        Blank
    }

    TileInfo darkTile = new TileInfo("Darkness", false, true, (int)TileType.Dark);
    TileInfo floorTile = new TileInfo("Stone Floor", false, true, (int)TileType.Floor);
    TileInfo wallTile = new TileInfo("Stone Wall", false, true, (int)TileType.Wall);
    TileInfo blankTile = new TileInfo("", false, true, (int)TileType.Blank);

    public MapInfo(int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;

        mapData = new TileInfo[columns, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                mapData[col, row] = darkTile;
            }
        }

        // Example room. This data would usually be procedural generated or read from a datasource.
        mapData[6, 4] = wallTile;
        mapData[7, 4] = wallTile;
        mapData[8, 4] = wallTile;
        mapData[9, 4] = wallTile;
        mapData[6, 5] = wallTile;
        mapData[7, 5] = floorTile;
        mapData[8, 5] = floorTile;
        mapData[9, 5] = wallTile;
        mapData[6, 6] = wallTile;
        mapData[7, 6] = floorTile;
        mapData[8, 6] = floorTile;
        mapData[9, 6] = wallTile;
        mapData[6, 7] = wallTile;
        mapData[7, 7] = wallTile;
        mapData[8, 7] = wallTile;
        mapData[9, 7] = wallTile;
    }

    public TileInfo GetTile(int col, int row)
    {
        return mapData[col, row];
    }

    public int GetTileTypeId(int col, int row)
    {
        return mapData[col, row].typeId;
    }

}
