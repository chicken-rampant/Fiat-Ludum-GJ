using System.ComponentModel;
using Unity.Properties;
using UnityEngine;

public class StatTracker : MonoBehaviour
{

    public static StatTracker instance;
    [CreateProperty]
    public int daysElapsed, timeElapsed, mood, hygeine, dilligence, charisma, money, 
    daysSinceGacha, daysSinceSlept, daysSinceCleaned;


    public void resetStats()
    {
        daysElapsed=0;
        timeElapsed=0;
        mood=0;
        money=1000;
        hygeine=-10;
        dilligence=-5;
        charisma=-15;
        daysSinceCleaned=0;
        daysSinceGacha=0;
        daysSinceSlept=0;
    }

    public void eventChecker()
    {
        //convert time passed -> full days
        if(timeElapsed>=3)
        {
            timeElapsed=0;
            daysElapsed++;
            daysSinceCleaned++;
            daysSinceGacha++;
            daysSinceSlept++;
        }

        if(daysElapsed%7==0)
        {
            //trigger allowance
        }

        if(daysSinceCleaned>=5)
        {
            //trigger mom angry scene
        }

        if(daysSinceSlept>=2)
        {
            //trigger sleep issue
        }

        if(mood<=-20)
        {
            //trigger forced gacha pull
        }

        if(daysElapsed > 50)
        {
            //trigger mom kicks you out event
        }

        if(daysSinceGacha>=3)
        {
            //die from gacha withdrawals
        }

        if(daysElapsed>=10&&hygeine<0)
        {
            //trigger cockroach infestation
        }

        if(mood<=-40)
        {
            //gacha addict
        }

        //go outside & good gacha ending separate

        if(daysElapsed>=15 && charisma<0)
        {
            //no aura death
        }

        if(charisma>25&&mood>25&&dilligence>25&&hygeine>25)
        {
            //isekai ending
        }

        if(dilligence>40)
        {
            //fries in the bag
        }

        if(hygeine>40)
        {
            //stay at home son
        }
    }

    public void randomize()
    {

    }

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        instance=this;
        DontDestroyOnLoad(gameObject);
        resetStats();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
