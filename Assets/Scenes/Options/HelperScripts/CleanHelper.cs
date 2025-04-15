using UnityEngine;

public class CleanHelper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker.instance.daysSinceCleaned=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
