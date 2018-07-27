using UnityEngine;
using System.Collections;

public enum LevelType
{
    None,
    Itzcuintlan,
    Tepectli,
    Iztepetl,
    Izteecayan,
    Paniecatacoyan,
    Timiminaloayan,
    Teocoyocualloa,
    Izmictlan,
    Chicunamictlan
}

public enum RoomType
{
    None,
    Normal,
    Torch,
    Puzzle,
    Horde,
    Item,
    Secret,
    Boss
}

public enum RoomInPath
{
    None,
    PathStart,
    CriticalPath,
    PathEnd,
    NonCriticalPath
}

public enum TilePosition
{
    TopLeft,
    Top,
    TopRight,
    MidLeft,
    MidMid,
    MidRight,
    BotLeft,
    Bot,
    BotRight
}