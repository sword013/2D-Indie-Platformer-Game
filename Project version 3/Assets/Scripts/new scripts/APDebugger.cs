using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class APDebugger : MonoBehaviour
{

    public Tilemap tilemap;
    public Transform PlayerPos;
    public GameObject MovingPlatformTest;

    void Start()
    {


        
        // BoundsInt bounds = tilemap.cellBounds;
        // TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        // Debug.Log(allTiles.Length);

        // List<TileBase> OccupiedTiles = new List<TileBase>();
        // foreach (TileBase tile in allTiles)
        // {
        //     if (tile!=null)
        //     {
        //         OccupiedTiles.Add(tile);
        //     }
        // }
        // Debug.Log(OccupiedTiles.Count);

        // for (int x = 0; x < bounds.size.x; x++) {
        //     for (int y = 0; y < bounds.size.y; y++) {
        //         TileBase tile = allTiles[x + y * bounds.size.x];
        //         if (tile != null) {
        //             Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
        //         } else {
        //             Debug.Log("x:" + x + " y:" + y + " tile: (null)");
        //         }
        //     }
        // }    
        // Debug.Log(tilemap.WorldToCell(new Vector3(20.5f,1,0)));
        // Debug.Log(tilemap.GetTile(new Vector3Int(20,1,0)));
        // Debug.Log((Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask)));
        // Debug.Log(tilemap.cellBounds);
    
    }

    private void FixedUpdate() {
        // Debug.Log("debuger "+tilemap.WorldToCell(PlayerPos.position));
    }

}
