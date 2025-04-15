using UnityEngine;

public class destroyMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(GameObject.Find("IntroMusic"));
        Destroy(GameObject.Find("StatTracker"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
