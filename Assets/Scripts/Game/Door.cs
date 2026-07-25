using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public Tilemap doorTilemap;
    public ItemInfo requiredKey;

    public void OpenDoor()
    {
        if (GameManager.player.HasItem(requiredKey))
        {
            doorTilemap.ClearAllTiles();
        }
        else
        {
            Debug.Log("You need a key.");
        }
    }
}
