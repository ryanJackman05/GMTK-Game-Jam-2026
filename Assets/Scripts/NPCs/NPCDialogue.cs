using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string charName;
    public string[] lines;

    Collider2D trigger;
    public ContactFilter2D filter = new ContactFilter2D();
    
    int currentLine; // current line of dialogue being displayed

    [SerializeField] bool inDialogue;
    void Start()
    {
        if (string.IsNullOrEmpty(charName)){
            charName = gameObject.name;
        }
        trigger = GetComponent<Collider2D>();
        filter.SetLayerMask(LayerMask.GetMask("Interactable"));
    }

    // Called by player to initiate or continue conversation
    public void Talk()
    {
        Debug.Log("Talked to " + charName);
        if(lines == null) return;
        
        if (!inDialogue){
            bool success = StartDialogue();
            if (!success){
                return;
            }
        }
        else{
            string nextLine = GetNextLine();
            if (nextLine == "EOL"){
                GameManager.gm.closeDialogue();
                inDialogue = false;
                return;
            }
            GameManager.gm.dialogue(nextLine, charName);
        }
    }
    public bool StartDialogue()
    {
        currentLine = 0;
        if (lines[currentLine] == null || lines.Length < 1) return false;
        
        // line found, start communication with GameManager for dialogue box management
        GameManager.gm.dialogue(lines[0], charName);
        inDialogue = true;
        return true;
    }
    public string GetNextLine() // returns the next dialogue line, without incrementing values
    {
        // if end of lines, end conversation
        if (currentLine == lines.Length - 1) return "EOL";
        
        // else continue
        currentLine++;
        return lines[currentLine];
    }
    
    // OLD METHODS
    string DialogueStatus()
    {
        return "Current Line: "+currentLine;
    }
}
