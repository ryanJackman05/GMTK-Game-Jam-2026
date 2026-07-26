using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int inventoryIndex;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GameManager.player.items[inventoryIndex])
            GameManager.gm.setDescText(GameManager.player.items[inventoryIndex].itemDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.gm.closeDescText();
    }
}
