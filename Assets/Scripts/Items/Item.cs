using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInfo itemInfo;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemInfo.itemIcon;
    }
}
