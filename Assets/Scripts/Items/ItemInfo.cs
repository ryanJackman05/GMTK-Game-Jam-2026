using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "ItemInfo")]

public class ItemInfo : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    
    public Sprite itemIcon;
}
