using UnityEngine;
using Yarn.Unity;

public class ArtHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("help");
        StatTracker.instance.money+=StatTracker.instance.dilligence*5;
        if(StatTracker.instance.dilligence>=0)
        {
            dialogueRunner.StartDialogue("Positive");
        }
        else
        {
            dialogueRunner.StartDialogue("Negative");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
