using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            int row = Mathf.FloorToInt(hitInfo.point.z / _tilemap3d.tileSize);
            // Debug.Log("You hovered tile: " + col + ", " + row);

            currentTileCoord.x = col;
            currentTileCoord.z = row;

            tileSelector.transform.position = currentTileCoord;
        }
        else
        {
            mouseOverTilemap = 0;
            _tileSelectorRenderer.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && mouseOverTilemap == 1)
        {
            int col = Mathf.FloorToInt(hitInfo.point.x / _tilemap3d.tileSize);
            int row = Mathf.FloorToInt(hitInfo.point.z / _tilemap3d.tileSize);
            Debug.Log("You selected tile: " + col + ", " + row);
        }
    }
}
