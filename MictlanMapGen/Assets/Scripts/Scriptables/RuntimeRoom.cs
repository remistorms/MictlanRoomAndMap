using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Runtime Room", menuName = "RemSO/RoomSO")]
public class RuntimeRoom : ScriptableObject {

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

}
