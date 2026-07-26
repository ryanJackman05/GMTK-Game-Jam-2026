using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject inventoryScreen;
    
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    
    [SerializeField] GameObject descBox;
    [SerializeField] TextMeshProUGUI descText;
    //[SerializeField] TextMeshProUGUI characterName;
    
    public static PlayerController player; // set by Player on spawn
    // Start is called before the first frame update
    void Start()
    {
        if (gm == null) gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dialogue(string text)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = text;
    }

    public void closeDialogue()
    {
        dialogueBox.SetActive(false);
    }

    public void setInfoText(string text)
    {
        descBox.SetActive(true);
        descText.text = text;
    }

    public void closeInfoText()
    {
        descBox.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

}
