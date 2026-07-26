using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public float timeRemaining = 120;
    bool inGame = true;
    
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject inventoryScreen;
    
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI characterName;
    
    [SerializeField] GameObject descBox;
    [SerializeField] TextMeshProUGUI descText;
    
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI timerText;
    
    // // // Guessing Vars
    [SerializeField] Camera guessCam;
    [SerializeField] GameObject guessMenu;
    
    
    public static PlayerController player; // set by Player on spawn
    // Start is called before the first frame update
    void Start()
    {
        if (gm == null) gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            timerText.text = ((int)timeRemaining).ToString();
        }
        else if (inGame){
            inGame = false;
            GuessScene();
        }
    }

    public void dialogue(string text, string name)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = text;
        characterName.text = name;
    }

    public void closeDialogue()
    {
        dialogueBox.SetActive(false);
    }

    public void setDescText(string text)
    {
        descBox.SetActive(true);
        descText.text = text;
    }

    public void closeDescText()
    {
        descBox.SetActive(false);
    }

    public void setInfoText(string text)
    {
        StopAllCoroutines();
        StartCoroutine(setTopText(text));
    }

    IEnumerator setTopText(string text)
    {
        infoText.gameObject.SetActive(true);
        infoText.text = text;
        yield return new WaitForSeconds(2f);
        infoText.gameObject.SetActive(false);
    }

    void GuessScene()
    {
        FindObjectOfType<CamFollow>().active = false;
        player.gameObject.SetActive(false); // disable player actions behind the scene
        Camera.main.gameObject.SetActive(false);
        guessCam.gameObject.SetActive(true);
            
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("NPC");
        GameObject[] anchors = GameObject.FindGameObjectsWithTag("Anchor");

        for (int i = 0; i < NPCs.Length; i++){
            GameObject NPC = NPCs[i];
            string index = NPC.name.Substring(4);
            foreach (GameObject anchor_ in anchors){

                if (anchor_.name.Contains(index)){
                    NPC.transform.position = anchor_.transform.position;
                }
            }
        }
        
        inventoryScreen.SetActive(false);
        timerText.gameObject.SetActive(false);
        infoText.gameObject.SetActive(false);
        guessMenu.SetActive(true);
    }

    public void WinOrLose(bool win)
    {
        if (win){
            SceneManager.LoadScene("win");
        }
        else{
            SceneManager.LoadScene("lose");
        }
    }
}
