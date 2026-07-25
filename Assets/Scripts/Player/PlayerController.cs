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
        interactionBox.GetContacts(hits);
        Collider2D interaction;
        // just grab first in list
        if (hits.Count > 0)
             interaction = hits[0];
        else
            return;

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
                if(!items[i] || !items[i].itemIcon) 
                    inventorySprites[i].sprite = null;
                else
                    inventorySprites[i].sprite = items[i].itemIcon;
            }
        }
    }

    void OnInventory(InputValue value)
    {
        Debug.Log(value.isPressed);
        inventoryScreen.SetActive(value.isPressed);
    }

    public void UseItem(ItemInfo itemInfo)
    {
        if (itemInfo == null) return;
        
        List<Collider2D> hits = new List<Collider2D>();
        interactionBox.GetContacts(hits);
        // just grab first in list
        Collider2D interaction = hits[0];
        
        if (interaction == null){}
        switch (itemInfo.name){
            case "Stake" :
                if (interaction.GetComponent<NPCDialogue>() != null){
                    bool wasVampire = interaction.GetComponent<NPCDialogue>().Kill();
                    if (wasVampire){
                        //GameManager.WinScreen();
                    }
                    else{
                        // GameManager.LoseScreen();
                    }
                }
                break;
            case "Master Bedroom Key":
                // TODO unlock logic
                break;
        }
    }
}
