using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {

    bool hasFirstLayer = false;
    bool canReadLayers = false;
    public int roomID;
    public int totalLayers;
    public string roomName;
    public string creatorName;
    public LevelType levelType;
    public RoomInPath roomInPath;
    public RoomType roomType;
    public Vector2Int roomSize;
    public Vector2Int roomPosition;
    //Level layers
    public Tilemap collisionLayer;
    public Tilemap triggerLayer;
    public List<GameObject> layers;
    public List<Tilemap> tilemaps;
    //Dictionary that holds a the position of each tilebase
    public Dictionary<Vector2Int, TileBase> tilesPosition = new Dictionary<Vector2Int, TileBase>();
    //Dictionary that holds a Dictionary of tiles position and respective layer int
    public Dictionary<int, Dictionary<Vector2Int, TileBase>> allLayersAndTiles = new Dictionary<int, Dictionary<Vector2Int, TileBase>>();
    //public Dictionary<<Dictionary<Vector2Int, TileBase>> roomTilemaps = new List<Dictionary<Vector2Int, TileBase>>();
    public TileBase startingBase;

    public List<RemTilemap> remTilemaps;

    public RuntimeRoom runtimeRoom;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateNewLayer();
            if (!hasFirstLayer)
            {
               // FillLayer(layerTilemaps[0]);
            }
            //
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToScriptable();
            ResetRoom();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
          
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ResetRoom();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadAllLayers();
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
                tilemaps[layerIndex].SetTile(cellPosition, startingBase);
                Debug.Log("CellPosition:" + cellPosition);
            }
        }
    }

    public void CreateNewLayer()
    {
        //Create New Layer
        GameObject newLayer = new GameObject("Layer_" + totalLayers.ToString());
        newLayer.AddComponent<RemTilemap>();
        newLayer.GetComponent<RemTilemap>().layerIndex = totalLayers;
        newLayer.transform.SetParent(transform);
        newLayer.AddComponent<Tilemap>();
        newLayer.AddComponent<TilemapRenderer>();
        newLayer.GetComponent<TilemapRenderer>().sortingOrder = totalLayers;
        layers.Add(newLayer);
        tilemaps.Add(newLayer.GetComponent<Tilemap>());
        remTilemaps.Add(newLayer.GetComponent<RemTilemap>());
        //layerTilemaps.Add(newLayer.GetComponent<Tilemap>());
        totalLayers++;
    }

    //Reset room to initial empty values
    void ResetRoom()
    {
        hasFirstLayer = false;
        canReadLayers = false;
        roomID = 0;
        totalLayers = 0;
        roomName = "";
        creatorName = "";
        levelType = LevelType.None;
        roomInPath = RoomInPath.None;
        roomType = RoomType.None;
        roomSize = new Vector2Int(20,20);
        roomPosition = new Vector2Int(0,0);
        tilemaps = new List<Tilemap>();
        tilemaps.Clear();
        layers = new List<GameObject>();
        layers.Clear();

        tilesPosition = new Dictionary<Vector2Int, TileBase>();
        tilesPosition.Clear();
        allLayersAndTiles = new Dictionary<int, Dictionary<Vector2Int, TileBase>>();
        allLayersAndTiles.Clear();

    }

    public void SaveToScriptable()
    {
        //Create Scriptable Object
        ReadAllLayers();
       // RuntimeRoom  newRoom = (RuntimeRoom)ScriptableObjectUtility.CreateAsset<RuntimeRoom>();
        //newRoom.layers = layers;
        //newRoom.tilemaps = tilemaps;
        FieldsToScriptableObject();
    }

  

    public void SetRoomPositionInMap(Vector3Int _position)
    {
        roomPosition = new Vector2Int(_position.x, _position.y);
    }


    #region //READ LAYERS
        //Reads a specific layer index, gets all tilebase positions and ads the entire dictionary to the all layers Dictionary
        void ReadLayer(int layerIndex)
        {
            if (totalLayers > 0)//Makes sure this doesnt die if nothing on the array
            {
                for (int i = -roomSize.x / 2; i < roomSize.x / 2; i++)
                {
                    for (int j = -roomSize.y / 2; j < roomSize.y / 2; j++)
                    {

                        Vector2Int tempPos = new Vector2Int(i, j);
                        TileBase tempTile = tilemaps[layerIndex].GetTile(new Vector3Int(tempPos.x, tempPos.y, 0));
                        tilesPosition.Add(tempPos, tempTile);
                       // Debug.Log("Tilebase:" + tempTile.ToString() + " placed at:[" + i + "," + j + "]" + "in layer:" + layerIndex);
                    }
                }
                
                Debug.Log("Finished reading");

            
            }
            else
            {
                Debug.Log("LayerTilemaps Array is empty");
            }
        }
        //Reads layer and ads the tilebase to the dictionary based on passed Tilemap
        void ReadLayer(Tilemap layerToRead)
        {
        if (totalLayers > 0)//Makes sure this doesnt die if nothing on the array
        {
            for (int i = -roomSize.x / 2; i < roomSize.x / 2; i++)
            {
                for (int j = -roomSize.y / 2; j < roomSize.y / 2; j++)
                {

                    Vector2Int tempPos = new Vector2Int(i, j);
                    TileBase tempTile = layerToRead.GetTile(new Vector3Int(tempPos.x, tempPos.y, 0));
                    tilesPosition.Add(tempPos, tempTile);
                    Debug.Log("Tilebase:" + tempTile.ToString() + " placed at:[" + i + "," + j + "]" + "from layer:" + layerToRead.gameObject.name);
                }
            }
            Debug.Log("Finished reading");

        }
        else
        {
            Debug.Log("LayerTilemaps Array is empty");
        }
    }
        //Reads all layers including trigger and collision and adds them to the Dictionary
        public void ReadAllLayers()
        {
            StartCoroutine(_ReadAllLayers());
        }
    #endregion

    IEnumerator _ReadAllLayers()
    {
        for (int i = 0; i < tilemaps.Count; i++)
        {
            ReadLayer(i);
            allLayersAndTiles.Add(i, tilesPosition);
            tilesPosition.Clear();
            Debug.Log("Finished Reading layer:" + i);
            yield return new WaitForSeconds(0.2f);
        }

        //ReadLayer(collisionLayer); // Collision layer will be saved on the tilesPosition.count -1
        yield return null;
        // ReadLayer(triggerLayer);// Trigger layer will be saved on the tilesPosition.count
        yield return null;
        Debug.Log("All layers read now");
        //SaveToScriptable();
        FieldsToScriptableObject();
    }

    void FieldsToScriptableObject()
    {
        Debug.Log("Asigning fields to inserted scriptable object");
        runtimeRoom.roomID = roomID;
        runtimeRoom.totalLayers = totalLayers;
        runtimeRoom.roomName = roomName;
        runtimeRoom.creatorName = creatorName;
        runtimeRoom.levelType = levelType;
        runtimeRoom.roomInPath = roomInPath;
        runtimeRoom.roomType = roomType;
        runtimeRoom.roomSize = roomSize;
        runtimeRoom.roomPosition = roomPosition;
        runtimeRoom.collisionLayer = collisionLayer;
        runtimeRoom.triggerLayer = triggerLayer;
        runtimeRoom.tilemaps = tilemaps;
        runtimeRoom.layers = layers;
        runtimeRoom.tilesPosition = tilesPosition;
        runtimeRoom.allLayersAndTiles = allLayersAndTiles;
    }

    void FieldsFromScriptableObject(RuntimeRoom _runtimeRoom)
    {
        Debug.Log("Asigning fields from Scriptable");
        roomID = _runtimeRoom.roomID;
        totalLayers = _runtimeRoom.totalLayers;
        roomName = _runtimeRoom.roomName;
        creatorName = _runtimeRoom.creatorName;
        levelType = _runtimeRoom.levelType;
        roomInPath = _runtimeRoom.roomInPath;
        roomType = _runtimeRoom.roomType;
        roomSize = _runtimeRoom.roomSize;
        roomPosition = _runtimeRoom.roomPosition;
        collisionLayer = _runtimeRoom.collisionLayer;
        triggerLayer = _runtimeRoom.triggerLayer;
        tilemaps = _runtimeRoom.tilemaps;
        layers = _runtimeRoom.layers;
        tilesPosition = _runtimeRoom.tilesPosition;
        allLayersAndTiles = _runtimeRoom.allLayersAndTiles;
    }
}

