using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.UnityIntegration;
using Ink.Runtime;

public class NPC : Interactable
{
    [SerializeField] private GameObject exclamation;
    [SerializeField] private TextAsset inkJSON;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Interact(){
        Debug.Log("NPC interacted with");

        if (!DialogueManager.Instance.dialogueIsPlaying)
        {
            DialogueManager.Instance.EnterDialogueMode(inkJSON, this);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        exclamation.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        exclamation.SetActive(false);
    }

    public void exitDialogue(Story currentStory)
    {

        if ((bool) currentStory.variablesState["send_to_arena"])
        {
            currentStory.variablesState["send_to_arena"] = false;
            GameManager.instance.StartBattle(1);
        }

    }

}
