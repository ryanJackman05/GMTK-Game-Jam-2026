using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ItemInfo[] items = new ItemInfo[8];
    [SerializeField] BoxCollider2D interactionBox;
    
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] Image[] inventorySprites = new Image[8];
    [SerializeField] GameObject itemPanelPrefab;
    
    [SerializeField] Sprite defaultSprite;

    public bool HasItem(ItemInfo item)
    {
        foreach (ItemInfo inventoryItem in items)
        {
            if (inventoryItem == item)
                return true;
        }

        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteract(InputValue value)
    {
        List<Collider2D> hits = new List<Collider2D>();
        int num = interactionBox.GetContacts(hits);
        
        // just grab first in list
        if (num == 0) return;
        Collider2D interaction = hits[0];

        if (interaction.GetComponent<NPCDialogue>() != null){
            interaction.GetComponent<NPCDialogue>().Talk();
        }
        else if (interaction.GetComponent<Item>() != null){
            Item item = interaction.GetComponent<Item>();
            item.gameObject.SetActive(false);
            
            if (item.itemInfo != null){
                for (int i = 0; i < 8; i++){
                    if (items[i] == null){
                        items[i] = item.itemInfo;
                        break;
                    }
                }
            }
            for (int i = 0; i < 8; i++){
                if(items[i] == null) inventorySprites[i].sprite = defaultSprite;
                else inventorySprites[i].sprite = items[i].itemIcon;
            }
        }
        else if (interaction.GetComponent<Door>() != null)
        {
            interaction.GetComponent<Door>().OpenDoor();
        }
    }

    void OnInventory(InputValue value)
    {
        Debug.Log(value.isPressed);
        inventoryScreen.SetActive(value.isPressed);
    }
}
