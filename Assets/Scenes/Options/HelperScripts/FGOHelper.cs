using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class FGOHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int roll = Random.Range(1, 101);
        if(roll<=60)
        {
            StatTracker.instance.mood-=4;
            dialogueRunner.StartDialogue("FGOBad");
        }
        else if(roll<=90)
        {
            StatTracker.instance.mood+=3;
            dialogueRunner.StartDialogue("FGODecent");
        }
        else if(roll<=97)
        {
            StatTracker.instance.mood+=8;
            dialogueRunner.StartDialogue("FGOGood");
        }
        else
        {
            SceneManager.LoadSceneAsync("GoodEnd");         
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
