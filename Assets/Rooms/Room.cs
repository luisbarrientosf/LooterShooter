using UnityEngine;
using System.Collections.Generic;

public enum RoomType { Start, Combat, Shop, Treasure, Boss }

public class Room : MonoBehaviour {
    public Vector2Int gridPosition;
    public RoomType roomType;
    public List<Vector2Int> connectedRooms = new List<Vector2Int>();
}