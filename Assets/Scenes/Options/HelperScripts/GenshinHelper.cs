using UnityEngine;
using Yarn.Unity;

public class GenshinHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker.instance.daysSinceGacha=0;
        int roll = Random.Range(1, 101);
        if(roll<=40)
        {
            StatTracker.instance.mood-=4;
            dialogueRunner.StartDialogue("GenshinBad");
        }
        else if(roll<=75)
        {
            StatTracker.instance.mood+=2;
            dialogueRunner.StartDialogue("GenshinDecent");
        }
        else if(roll<=92)
        {
            StatTracker.instance.mood+=5;
            dialogueRunner.StartDialogue("GenshinGood");
        }
        else
        {
            StatTracker.instance.mood+=8;
            dialogueRunner.StartDialogue("GenshinBest");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
