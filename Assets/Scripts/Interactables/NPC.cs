using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    private Dialogue dialogue;
    [SerializeField] private GameObject exclamation;

    // Start is called before the first frame update
    void Start()
    {
        CreateTestDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(){
        Debug.Log("NPC interacted with");
        DialogueManager.instance.StartConversation(dialogue);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        exclamation.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        exclamation.SetActive(false);
    }

    void CreateTestDialogue()
    {
        Line line0 = new Line(0, "This is the starting line (0)", new List<Option>{new Option("FIGHT (-1, starts fight)", -1, 1), new Option("Leads to line 1", 1), new Option("Leads to line 2", 2), new Option("Ends conversation (-1)", -1)});
        Line line1 = new Line(1, "Just another line (1)", new List<Option>{new Option("Leads to line 1", 1), new Option("Leads to line 0", 0), new Option("Leads to line 2", 2), new Option("Ends conversation (-1)", -1)});
        Line line2 = new Line(2, "Wow, more lines! (2)", new List<Option>{new Option("Ends conversation (-1)", -1), new Option("Leads to line 0", 0), new Option("Leads to line 1", 1), new Option("Leads end line 2", 2)});
        
        dialogue = new Dialogue(new List<Line>{line0, line1, line2});
    }
}
