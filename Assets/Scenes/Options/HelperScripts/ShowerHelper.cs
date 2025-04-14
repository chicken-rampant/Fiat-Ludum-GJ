using UnityEngine;
using Yarn.Unity;

public class ShowerHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(StatTracker.instance.hygeine < 0)
        {
            dialogueRunner.StartDialogue("ShowerBad");
        }
        else
        {
            StatTracker.instance.mood+=5;
            StatTracker.instance.hygeine+=3;
            dialogueRunner.StartDialogue("ShowerGood");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
