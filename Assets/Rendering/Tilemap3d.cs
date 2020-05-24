using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Tilemap3d : MonoBehaviour
{
    public int columns = 16;
    public int rows = 12;
    public float tileSize = 1.0f;
    public int tileResolution = 32;
    public Texture2D tileAtlas;

    public float yMin = 0f;
    public float yMax = 0f;

    // Start is called before the first frame update
    void Start()
    {
        BuildMesh();
    }

    Color[][] SliceTileAltasIntoTiles()
    {
        int numberOfColumnsInTileAtlas = tileAtlas.width / tileResolution;
        int numberOfRowsInTileAtlas = tileAtlas.height / tileResolution;
        int numberOfTilesInTileAtlas = numberOfColumnsInTileAtlas * numberOfRowsInTileAtlas;

        Color[][] tiles = new Color[numberOfTilesInTileAtlas][];

        for (int row = 0; row < numberOfRowsInTileAtlas; ++row)
        {
            for (int col = 0; col < numberOfColumnsInTileAtlas; ++col)
            {
                tiles[row * numberOfColumnsInTileAtlas + col] = tileAtlas.GetPixels(col * tileResolution, row * tileResolution, tileResolution, tileResolution);
            }
        }

        return tiles;
    }

    public void BuildTexture()
    {
        MapInfo mapInfo = new MapInfo(columns, rows);

        int textureWidth = columns * tileResolution;
        int textureHeight = rows * tileResolution;

        Texture2D texture = new Texture2D(textureWidth, textureHeight);

        Color[][] tiles = SliceTileAltasIntoTiles();

        // To achieve a left to right, top to bottom tile flow count from the number of rows down to 0. The inner loop counts from 0 up to the number of columns.
        for (int row = rows - 1; row > -1; --row)
        {
            for (int col = 0; col < columns; ++col)
            {
                Color[] tile = tiles[mapInfo.GetTileTypeId(col, row)];
                texture.SetPixels(col * tileResolution, row * tileResolution, tileResolution, tileResolution, tile);
            }
        }

        texture.filterMode = FilterMode.Point; // Retro luv
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterials[0].mainTexture = texture;
    }

    public void BuildMesh()
    {
        int numberOfTiles = columns * rows;
        int numberOfTriangles = numberOfTiles * 2;
        int verticesColumns = columns + 1;
        int verticesRows = rows + 1;
        int numberOfVertices = verticesColumns * verticesRows; 

        // Mesh data
        Vector3[] normals = new Vector3[numberOfVertices];
        int[] triangles = new int[numberOfTriangles * 3];
        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uv = new Vector2[numberOfVertices];

        int col, row;

        // Create vertices, normals, uv's
        for (row = verticesRows - 1; row > -1; --row)
        {
            for (col = 0; col < verticesColumns; ++col)
            {
                int tileIndex = row * verticesColumns + col;

                vertices[tileIndex] = new Vector3(col * tileSize, Random.Range(yMin, yMax), -row * tileSize);
                normals[tileIndex] = Vector3.up;
                uv[tileIndex] = new Vector2((float)col / columns, 1 - (float)row / rows);
            }
        }

        // Create triangles
        for (row = rows - 1; row > -1; --row)
        {
            for (col = 0; col < columns; ++col)
            {
                int tileIndex = row * columns + col;
                int triangleOffset = tileIndex * 6;

                triangles[triangleOffset + 0] = row * verticesColumns + col;
                triangles[triangleOffset + 2] = row * verticesColumns + col + verticesColumns + 0;
                triangles[triangleOffset + 1] = row * verticesColumns + col + verticesColumns + 1;

                triangles[triangleOffset + 3] = row * verticesColumns + col;
                triangles[triangleOffset + 5] = row * verticesColumns + col + verticesColumns + 1;
                triangles[triangleOffset + 4] = row * verticesColumns + col + 1;
            }
        }

        // Create mesh
        Mesh mesh = new Mesh();
        mesh.name = "Tilemap3d";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        // Assign our mesh to collider/filter/renderer
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        meshCollider.sharedMesh = mesh;
        meshFilter.mesh = mesh;

        BuildTexture();
    }

}
