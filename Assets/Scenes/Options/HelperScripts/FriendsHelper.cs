using UnityEngine;
using Yarn.Unity;

public class FriendsHelper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DialogueRunner dialogueRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(StatTracker.instance.charisma<0)
        {
            dialogueRunner.StartDialogue("CharismaLow");
            StatTracker.instance.charisma-=2;
        }
        else
        {
            dialogueRunner.StartDialogue("CharismaHigh");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
