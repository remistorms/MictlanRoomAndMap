using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {

    public int roomID;
    public int totalLayers;
    public string roomName;
    public string creatorName;
    public LevelType levelType;
    public RoomInPath roomInPath;
    public RoomType roomType;
    public Vector2Int roomSize;
    public Vector2Int roomPosition;
    public Tilemap collisionLayer;
    public Tilemap triggerLayer;
    public List<Tilemap> layerTilemaps;
    public Dictionary<Vector2Int, TileBase> tilesPosition;
    public List<Dictionary<Vector2Int, TileBase>> roomTilemaps;
    public TileBase startingBase;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateNewLayer();
            FillLayer(layerTilemaps[0]);
        }
    }

    //Fills the selected tilemap with the starting base tile
    public void FillLayer(Tilemap tilemap)
    {
        for (int i = -roomSize.x / 2; i < roomSize.x/2; i++)
        {
            for (int j = -roomSize.y / 2; j < roomSize.y / 2; j++)
            {

                Vector3Int cellPosition = new Vector3Int(i, j, 0);
                tilemap.SetTile(cellPosition, startingBase);
                Debug.Log("CellPosition:" + cellPosition);
            }
        }
    }

    public void FillLayer(int layerIndex)
    {
        for (int i = -roomSize.x / 2; i < roomSize.x / 2; i++)
        {
            for (int j = -roomSize.y / 2; j < roomSize.y / 2; j++)
            {

                Vector3Int cellPosition = new Vector3Int(i, j, 0);
                layerTilemaps[layerIndex].SetTile(cellPosition, startingBase);
                Debug.Log("CellPosition:" + cellPosition);
            }
        }
    }

    public void CreateNewLayer()
    {
        //Create New Layer
        GameObject newLayer = new GameObject("Layer_" + totalLayers.ToString());
        newLayer.transform.SetParent(transform);
        newLayer.AddComponent<Tilemap>();
        newLayer.AddComponent<TilemapRenderer>();
        layerTilemaps.Add(newLayer.GetComponent<Tilemap>());
        totalLayers++;
    }

    public void ResetRoom()
    {
        roomID = 0;
        totalLayers = 0;
        roomName = "New Room";
        creatorName = "Created By: ";
        levelType = LevelType.None;
        roomInPath = RoomInPath.None;
        roomType = RoomType.None;
        roomSize = new Vector2Int(20, 20); ;
        roomPosition = new Vector2Int(0, 0);
        roomTilemaps.Clear();
        tilesPosition = new Dictionary<Vector2Int, TileBase>();
        tilesPosition.Clear();
        roomTilemaps = new List<Dictionary<Vector2Int, TileBase>>();
        roomTilemaps.Clear();
    }
}

