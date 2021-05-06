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
        GameManager.instance.changeHelpText("Press SPACE to open the door");
        GameManager.instance.switchDoor();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        GameManager.instance.hideHelpText();
        
    }
}
