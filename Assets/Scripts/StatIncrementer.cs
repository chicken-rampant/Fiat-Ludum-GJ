using UnityEngine;

public class StatIncrementer : MonoBehaviour
{
    [SerializeField] bool timeIncrement=true;

    [SerializeField] int moodIncrement=0;

    [SerializeField] int moneyIncrement=0;

    [SerializeField] int hygeineIncrement=0;

    [SerializeField] int dilligenceIncrement=0;

    [SerializeField] int charismaIncrement=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(timeIncrement){StatTracker.instance.timeElapsed++;}
        StatTracker.instance.mood+=moodIncrement;
        StatTracker.instance.money+=moneyIncrement;
        StatTracker.instance.hygeine+=hygeineIncrement;
        StatTracker.instance.charisma+=charismaIncrement;
        StatTracker.instance.dilligence+=dilligenceIncrement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
