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
            SoundManager.sm.PlayClip("snd_door_unlock");
        }
        else
        {
            GameManager.gm.setInfoText("You need a key.");
        }
    }
}
