using UnityEngine;
using Yarn.Unity;

public class AzurLaneHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker.instance.daysSinceGacha=0;
        int roll = Random.Range(1, 101);
        if(roll<=10)
        {
            StatTracker.instance.mood-=2;
            dialogueRunner.StartDialogue("AzurBad");
            //bad rng
        }
        else if(roll<=70)
        {
            StatTracker.instance.mood+=2;
            dialogueRunner.StartDialogue("AzurDecent");
            //decent rng
        }
        else if(roll<=90)
        {
            StatTracker.instance.mood+=3;
            dialogueRunner.StartDialogue("AzurGood");
            //high rng
        }
        else
        {
            StatTracker.instance.mood+=6;
            dialogueRunner.StartDialogue("AzurBest");
            //highest rng
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
