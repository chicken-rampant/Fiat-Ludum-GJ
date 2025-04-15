using System.ComponentModel;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatTracker : MonoBehaviour
{

    public static StatTracker instance;
    [CreateProperty]
    public int daysElapsed, timeElapsed, mood, hygeine, dilligence, charisma, money, 
    daysSinceGacha, daysSinceSlept, daysSinceCleaned, daysSinceForcedGachaPull;


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
            daysSinceForcedGachaPull++;
        }

        if(daysElapsed%7==0)
        {
            SceneManager.LoadSceneAsync("Allowance");
            return;
            //trigger allowance
        }

        if(daysSinceCleaned>=5)
        {
            daysSinceCleaned=0;
            SceneManager.LoadSceneAsync("AsianMom");
            return;
            //trigger mom angry scene
        }

        if(daysSinceSlept>=2)
        {
            daysSinceSlept=1;
            SceneManager.LoadSceneAsync("SleepDeprived");
            return;
            //trigger sleep issue
        }

        if(mood<=-20 &&daysSinceForcedGachaPull>=2)
        {
            daysSinceForcedGachaPull=0;
            SceneManager.LoadSceneAsync("StressGachaing");
            return;
            //trigger forced gacha pull
        }

        if(daysElapsed > 50)
        {
            SceneManager.LoadSceneAsync("Disowned");
            return;
            //trigger mom kicks you out event
        }

        if(daysSinceGacha>=3)
        {
            SceneManager.LoadSceneAsync("Withdrawals");
            return;
            //die from gacha withdrawals
        }

        if(daysElapsed>=10&&hygeine<0)
        {
            SceneManager.LoadSceneAsync("Cockroach");
            return;
            //trigger cockroach infestation
        }

        if(mood<=-40)
        {
            SceneManager.LoadSceneAsync("CreditCardDebt");
            return;
            //gacha addict
        }

        //go outside & good gacha ending separate

        if(charisma>25&&mood>25&&dilligence>25&&hygeine>25)
        {
            SceneManager.LoadSceneAsync("Isekai");
            return;
        }

        if(dilligence>40)
        {
            SceneManager.LoadSceneAsync("FriesInTheBag");
            return;
            //fries in the bag
        }

        if(hygeine>40)
        {
            SceneManager.LoadSceneAsync("StayAtHomeSon");
            return;
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
