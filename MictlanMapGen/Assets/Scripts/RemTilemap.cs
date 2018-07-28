using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class RemTilemap : MonoBehaviour {

    public int layerIndex;
    public Dictionary<Vector2Int, TileBase> layerTiles;
}
