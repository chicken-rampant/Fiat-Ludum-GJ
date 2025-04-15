using UnityEngine;

public class ending : MonoBehaviour
{
    public Animator transition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restart()
    {
        StatTracker.instance.resetStats();
        transition.GetComponent<sceneManager>().advanceScene();
    }
}
