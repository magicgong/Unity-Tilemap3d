using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Tilemap3d))]

public class Tilemap3dMouse : MonoBehaviour
{
    Tilemap3d _tilemap3d;
    Vector3 currentTileCoord;
    public Transform tileSelector;
    MeshRenderer _tileSelectorRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _tilemap3d = GetComponent<Tilemap3d>();
        _tileSelectorRenderer = tileSelector.GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        int mouseOverTilemap = 0;

        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            mouseOverTilemap = 1;
            _tileSelectorRenderer.enabled = true;

            int col = Mathf.FloorToInt(hitInfo.point.x / _tilemap3d.tileSize);
            // Unity’s z axis is inverted from the top to bottom tile flow.
            int rowInverted = Mathf.FloorToInt(hitInfo.point.z / _tilemap3d.tileSize);

            currentTileCoord.x = col;
            currentTileCoord.z = rowInverted; // When dealing with the coordinates (for the z axis), use the inverted row.

            tileSelector.transform.position = currentTileCoord;

            // When dealing with tiles, invert rowInverted to get the correct row.
            //int row = invertRow(rowInverted);
            //Debug.Log("You hovered tile: " + col + ", " + row);
        }
        else
        {
            mouseOverTilemap = 0;
            _tileSelectorRenderer.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && mouseOverTilemap == 1)
        {
            int col = Mathf.FloorToInt(hitInfo.point.x / _tilemap3d.tileSize);
            // Unity’s z axis is inverted from the top to bottom tile flow.
            int rowInverted = Mathf.FloorToInt(hitInfo.point.z / _tilemap3d.tileSize);

            // When dealing with tiles, invert rowInverted to get the correct row.
            int row = invertRow(rowInverted);
            Debug.Log("You selected tile: " + col + ", " + row + " - " + MapInfo.mapData[col, row].name);
        }
    }

    int invertRow(int rowInverted)
    {
        return (rowInverted * -1) - 1;
    }
}
