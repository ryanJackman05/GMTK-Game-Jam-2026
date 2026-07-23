using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string[] lines;
    public int[] chainIndexes; // indexes for first line of each unique chain/conversation of standard dialogue lines.
                                       // last index should mark the final line of the last dialogue chain, all other indexes mark start of chain
    public int requestActivatedChain; // index of the chain that the item request will start on

    public GameObject[] desiredItems;
    Collider2D trigger;
    public ContactFilter2D filter = new ContactFilter2D();
    public int recieveItem, wrongItem; // indexes for each unique chain of request-related dialogue lines. wrongItem must be the final chain out of all of lines[]
    int currentLine, currentChain; // current chain starting line. can only be a value from chainIndexes

    public bool inDialogue, dialogueExhausted, inRequestDialogue, requestActivated;
    public GameObject requestEndedTarget;
    public string requestEndMessage;
    void Start()
    {
        trigger = GetComponent<Collider2D>();
        filter.SetLayerMask(LayerMask.GetMask("Grabbables"));
    }
    public string StartDialogueChain(int chainIndex)
    {
        foreach(int i  in chainIndexes)
        {
            if(i == chainIndex)
            {
                currentLine = chainIndexes[chainIndex];
                return GetDialogue(currentLine);
            }
        }
        return "invalid chain index";
    }
    public string StartNextDialogueChain()
    {
        //foreach (int i in dialogueChainIndexes)
        //{
        //    if (i == currentChain+1)
        //    {
        //        return GetDialogue(currentDialogue);
        //    }
        //}
        //return "invalid dialogue index";

        // item request check before standard dialogue starts
        if (requestActivated)
        {
            // do initial request item check
            List<Collider2D> items = new List<Collider2D>();
            trigger.GetContacts(filter, items);

            if (items.Count > 1) // if there is a Grabbable item nearby
            {
                // check for request items nearby
                currentLine = wrongItem;
                foreach (Collider2D c in items)
                {
                    foreach (GameObject g in desiredItems)
                    {
                        if (c.gameObject == g) // if items contains a desiredItem
                        {
                            currentLine = recieveItem;
                        }
                    }
                }
                inRequestDialogue = true;
            }
        }
        else if (dialogueExhausted) // if request not applicable, default to final chain check
        {
            currentLine = currentChain = chainIndexes[chainIndexes.Length-2]; // set to second last chain index, AKA final chain
        }

        inDialogue = true;
        return GetDialogue(currentLine);
    }
    public string GetDialogue(int index)
    {
        return lines[index];
    }
    public string GetNextLine() // returns the next dialogue line, without incrementing values
    {
        return GetDialogue(currentLine+1);
    }
    public string Next() // Main dialogue action, checks for status of current line and for NPC request items
    {
        // request check before standard dialogue starts
        if (requestActivated && inRequestDialogue)
        {
            // this code should work regardless of recieveItem or wrongItem scenario
            currentLine++;
            // Employ NEXTLINE PROCESSING in the context of the two request chain indexes
            if (currentLine == wrongItem || currentLine == lines.Length) // if next line = wrongItem OR is beyond the LAST dialogue line (request chain ended)
            {
                if (currentLine == wrongItem && requestEndedTarget != null)
                {
                    // request fulfilled, send message to target
                    requestEndedTarget.SendMessage(requestEndMessage, SendMessageOptions.DontRequireReceiver);
                }
                inRequestDialogue = false;
                currentLine = currentChain; // revert to last chain. should be a chain from chainIndexes
                return "EOC";
            }
            else
            {
                return GetDialogue(currentLine);
            }
        }

        // currentLine is not a request chain index, check for normal dialogue

        // PRE-LINE PROCESSING //////////////////////////////////////////////////////////////////////////////////////////

        // check if currentLine is final chain index before incrementing, and revert to other number or close dialogue.
        if (currentLine == chainIndexes[chainIndexes.Length - 1]) // if current line (line that has already been said) is the final chain index (final line has already been stated)
        {
            currentLine = currentChain = chainIndexes[chainIndexes.Length - 2]; // set to second last chain index, AKA final chain
            inDialogue = false;
            return "EOC"; // close dialogue. currentLine is now prepared for the dialogue to be opened again
        }

        // NEXT LINE PROCESSING //////////////////////////////////////////////////////////////////////////////////////////
        currentLine++;
        inDialogue = true; // assuming true
        for (int i = 0; i < chainIndexes.Length; i++) // foreach chain
        {
            if (chainIndexes[i] == currentLine) // if next dialogue is a chain index
            {
                if(i == chainIndexes.Length-1) // if next dialogue is LAST chain index
                {
                    dialogueExhausted = true; // begin looping final chain, Dark Souls style
                    return GetDialogue(currentLine);
                }

                // otherwise currentLine is now the start of the next chain, and current chain is about to end
                inDialogue = false;
                if (currentChain == requestActivatedChain) // was this chain the request activation chain??
                {
                    requestActivated = true;
                }
                currentChain = currentLine;
                return "EOC"; // code for End Of Chain, checked by GameManager for closing dialogue box
            }
        }
        

        // otherwise return next line
        return GetDialogue(currentLine);
    }
    string DialogueStatus()
    {
        return "Last Line: "+currentLine;
    }
}
