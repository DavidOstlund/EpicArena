using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public Dictionary<int, Line> lineDict = new Dictionary<int, Line>();

    public Dialogue(List<Line> lineList)
    {
        foreach (Line line in lineList) {
            lineDict.Add(line.id, line);
        }
    }

    public void StartConversation()
    {
        
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
