using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public int id;
    public string text;
    public List<Option> optionList = new List<Option>();

    public Line(int id, string text, List<Option> optionList)
    {
        this.id = id;
        this.text = text;
        this.optionList = optionList;
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
