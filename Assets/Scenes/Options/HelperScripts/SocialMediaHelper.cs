using UnityEngine;
using Yarn.Unity;

public class SocialMediaHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int option = Random.Range(1,4);
        if(option == 1)
        {
            StatTracker.instance.charisma-=2;
            StatTracker.instance.mood-=2;
            dialogueRunner.StartDialogue("TwitterDialogue");
        }
        if(option == 2)
        {
            StatTracker.instance.dilligence-=1;
            StatTracker.instance.mood+=4;
            dialogueRunner.StartDialogue("RedditDialogue");
        }
        if(option ==3)
        {
            StatTracker.instance.dilligence+=3;
            StatTracker.instance.mood-=2;
            dialogueRunner.StartDialogue("YoutubeDialogue");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
