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

    //public RuntimeRoom testRuntimeRoom;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateNewLayer();
            FillLayer(layerTilemaps[0]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToScriptable();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadFromScriptable(testRuntimeRoom);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRoom();
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

    public void SaveToScriptable()
    {
        //Create Scriptable Object
        RuntimeRoom  newRoom = (RuntimeRoom)ScriptableObjectUtility.CreateAsset<RuntimeRoom>();

        newRoom.roomID = roomID;
        newRoom.totalLayers = totalLayers;
        newRoom.roomName = roomName;
        newRoom.creatorName = creatorName;
        newRoom.levelType = levelType;
        newRoom.roomInPath = roomInPath;
        newRoom.roomType = roomType;
        newRoom.roomSize = roomSize;
        newRoom.roomPosition = roomPosition;
        newRoom.roomTilemaps = roomTilemaps;
        newRoom.tilesPosition = tilesPosition;
        newRoom.roomTilemaps = roomTilemaps;
        //Save collision and trigger layers
        newRoom.collisionLayer = collisionLayer;
        newRoom.triggerLayer = triggerLayer;

        Debug.Log("Created Room Scriptable Object");
    }

    public void LoadFromScriptable(RuntimeRoom _runtimeRoom)
    {
        roomID = _runtimeRoom.roomID;
        totalLayers = _runtimeRoom.totalLayers;
        roomName = _runtimeRoom.roomName;
        creatorName = _runtimeRoom.creatorName;
        levelType = _runtimeRoom.levelType;
        roomInPath = _runtimeRoom.roomInPath;
        roomType = _runtimeRoom.roomType;
        roomSize = _runtimeRoom.roomSize;
        roomPosition = _runtimeRoom.roomPosition;
        roomTilemaps = _runtimeRoom.roomTilemaps;
        tilesPosition = _runtimeRoom.tilesPosition;
        roomTilemaps = _runtimeRoom.roomTilemaps;
        //Gets the collision and Trigger layers
        collisionLayer = _runtimeRoom.collisionLayer;
        triggerLayer = _runtimeRoom.triggerLayer;
    }

}

