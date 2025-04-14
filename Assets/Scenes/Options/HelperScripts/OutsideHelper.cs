using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class OutsideHelper : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(StatTracker.instance.charisma+StatTracker.instance.dilligence+StatTracker.instance.hygeine<20)
        {
            SceneManager.LoadSceneAsync("GrassAllergy");
        }
        else
        {
            dialogueRunner.StartDialogue("outsideGood");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
