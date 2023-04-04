using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance = null;
    public DialogueInputHandler dialogueInputHandler;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text mainText;
    [SerializeField] private Text option1;
    [SerializeField] private Text option2;
    [SerializeField] private Text option3;
    [SerializeField] private Text option4;

    private Dialogue currentDialogue = null;
    private Line currentLine = null;
    private NPC currentNPC = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this) {
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        dialogueInputHandler = this.GetComponent<DialogueInputHandler>();
        dialogueInputHandler.enabled = false;
    }

    public void StartConversation(Dialogue dialogue, NPC sendingNPC)
    {
        currentNPC = sendingNPC;
        currentDialogue = dialogue;

        GameManager.instance.PlayerInputHandlerToggle(false);
        dialogueInputHandler.enabled = true;

        dialogueBox.SetActive(true);
        currentLine = currentDialogue.lineDict[0];
        fillDialogueBox();
    }

    public void nextLine(int chosenOption)
    {
        Option option = currentLine.optionList[chosenOption];
        int destination = option.destination;

        if (destination == -1) {
            currentNPC.RecieveDialogFlag(option.sendBackFlag);
            endConversation();
        } else {
            currentLine = currentDialogue.lineDict[destination];
            fillDialogueBox();
        }
    }

    private void fillDialogueBox()
    {
        if (currentLine == null){
            Debug.Log("Trying to fill the dialogue box when no line is chosen");
            return;
        }

        mainText.text = currentLine.text;
        option1.text = currentLine.optionList[0].text;
        option2.text = currentLine.optionList[1].text;
        option3.text = currentLine.optionList[2].text;
        option4.text = currentLine.optionList[3].text;
    }

    private void endConversation()
    {
        dialogueBox.SetActive(false);
        currentDialogue = null;
        currentLine = null;

        dialogueInputHandler.enabled = false;
        GameManager.instance.PlayerInputHandlerToggle(true);
    }
}
