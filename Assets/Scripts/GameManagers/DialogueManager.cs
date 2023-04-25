using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header ("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private Story currentStory;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject endConversationButton;
    public bool dialogueIsPlaying { get; private set; }
    private TextMeshProUGUI[] choicesText;
    public static DialogueManager Instance { get; private set; }
    private DialogueVariables dialogueVariables;
    private NPC currentNPC;



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        Instance = this;
        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
    }

    public static DialogueManager GetInstance()
    {
        return Instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get all of the choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, NPC npc)
    {
        InputManager.Instance.SwitchToActionMap("Dialogue");
        currentNPC = npc;
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }

    public void ExitDialogueMode()
    {        
        endConversationButton.gameObject.SetActive(false);
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        currentNPC.exitDialogue(currentStory);

        InputManager.Instance.SwitchToActionMap("Player");
    }

    private void ContinueStory()
    {
        dialogueText.text = currentStory.Continue();

        if (currentStory.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else 
        {
            foreach (GameObject choice in choices)
            {
                choice.SetActive(false);
            }
            endConversationButton.gameObject.SetActive(true);
            StartCoroutine(InputManager.Instance.SelectButton(endConversationButton));
        }
    }

    private async void DisplayChoices()
    {
        Debug.Log("currentStory.canContinue: " + currentStory.canContinue);
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        
        StartCoroutine(InputManager.Instance.SelectButton(choices[0]));
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }    

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variablesValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variablesValue);
        if (variablesValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);            
        }
        return variablesValue;
    }

}
