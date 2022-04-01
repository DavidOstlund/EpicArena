using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option
{

    public string text;
    public int destination;
    public int sendBackFlag = 0;

    public Option(string text, int destination)
    {
        this.text = text;
        this.destination = destination;
    }

    public Option(string text, int destination, int sendBackFlag)
    {
        this.text = text;
        this.destination = destination;
        this.sendBackFlag = sendBackFlag;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
