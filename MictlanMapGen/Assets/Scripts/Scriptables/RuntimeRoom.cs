using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Runtime Room", menuName = "RemSO/Rooms")]
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
    public Tilemap collisionLayer;
    public Tilemap triggerLayer;
    public List<Tilemap> layerTilemaps;
    public Dictionary<Vector2Int, TileBase> tilesPosition;
    public List<Dictionary<Vector2Int, TileBase>> roomTilemaps;
    public TileBase startingBase;
}
