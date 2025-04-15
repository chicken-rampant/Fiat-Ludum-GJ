using UnityEngine;

public class SleepHelper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker.instance.daysSinceSlept=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
