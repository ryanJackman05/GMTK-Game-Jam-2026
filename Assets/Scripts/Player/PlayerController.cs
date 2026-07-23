using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BoxCollider2D interactionBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteract(InputValue value)
    {
        List<Collider2D> hits = new List<Collider2D>();
        interactionBox.GetContacts(hits);
        
        // just grab first in list
        Collider2D interaction = hits[0];

        if (interaction.GetComponent<NPCDialogue>() != null){
            interaction.GetComponent<NPCDialogue>().Talk();
        }
        else if(true){
            // TODO item
        }
    }
}
