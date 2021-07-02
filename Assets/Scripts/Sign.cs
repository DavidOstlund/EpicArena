using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameManager.instance.changeHelpText("Walk inside at your own risk.");
        GameManager.instance.switchDoor();
        Invoke("HideText", 5.0f);
    }

    public void HideText()
    {
        GameManager.instance.hideHelpText();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //GameManager.instance.hideHelpText();
        
    }
}
