using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public float timeRemaining = 120;
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject inventoryScreen;
    
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    
    [SerializeField] GameObject descBox;
    [SerializeField] TextMeshProUGUI descText;
    
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI timerText;
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
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            timerText.text = ((int)timeRemaining).ToString();
        }
        else{
            // TODO switch to select screen
        }
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
