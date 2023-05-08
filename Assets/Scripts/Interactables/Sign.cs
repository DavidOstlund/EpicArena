using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        GameManager.Instance.changeHelpText("Walk inside at your own risk.");
        GameManager.Instance.switchDoor();
        Invoke("HideText", 5.0f);
    }

    public void HideText()
    {
        GameManager.Instance.hideHelpText();
    }
}
