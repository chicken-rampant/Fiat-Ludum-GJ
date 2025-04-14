using UnityEngine;
using Yarn.Unity;

public class ProsekaiHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int roll = Random.Range(1, 101);
        if(roll<=25)
        {
            StatTracker.instance.mood-=3;
            dialogueRunner.StartDialogue("ProsekaiBad");
        }
        else if(roll<=65)
        {
            StatTracker.instance.mood+=3;
            dialogueRunner.StartDialogue("ProsekaiDecent");            
        }
        else if(roll<=92)
        {
            StatTracker.instance.mood+=5;
            dialogueRunner.StartDialogue("ProsekaiGood");
        }
        else
        {
            StatTracker.instance.mood+=6;
            dialogueRunner.StartDialogue("ProsekaiBest");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
