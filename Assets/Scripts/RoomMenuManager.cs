using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

public class RoomMenuManager : MonoBehaviour
{
    private UIDocument document;
    private List<Button> buttons = new List<Button>();
    private VisualElement sideMenu, gachaMenu, sideMenuImage, sideMenuStatChanges;
    private Label sideMenuCard, sideMenuDescription;
    Dictionary<string, HoverBehaviorContainer> buttonHoverMap = new Dictionary<string, HoverBehaviorContainer>();
    
    private void initHashmaps()
    {
        buttonHoverMap.Add("MonitorMenu", new HoverBehaviorContainer ("Monitor", "Gain access to all your favorite online activities"));
        buttonHoverMap.Add("Outside", new HoverBehaviorContainer("The Outside", "It frightens you. Who knows what will happen when you venture out there..."));
        buttonHoverMap.Add("Cleaning", new HoverBehaviorContainer("Clean", "Give in to your mom's wishes and clean your room. Increases diligence and hygeine."));
        buttonHoverMap.Add("Sleep", new HoverBehaviorContainer("Sleeping", "Let your body rest, restoring energy and mental fortitude"));
        buttonHoverMap.Add("Shower", new HoverBehaviorContainer("Shower", "It's intimidating, but it is vital for survival. Increases hygeine and mood"));
        buttonHoverMap.Add("CharismaStat", new HoverBehaviorContainer("Charisma", "One of your primary stats. Increased through talking with friends and other various methods..."));
        buttonHoverMap.Add("MoodStat", new HoverBehaviorContainer("Mood", "One of your primary stats, and the one that fluctuates the most. Keep it up, or bad things may happen..."));
        buttonHoverMap.Add("DiligenceStat", new HoverBehaviorContainer("Diligence", "One of your primary stats. Increased by drawing and other various activities..."));
        buttonHoverMap.Add("HygeineStat", new HoverBehaviorContainer("Hygeine", "Another one of your primary stats. Increased by cleaning, showering, and other activities. I wonder what happens if it's negative by day 10..."));

    }

    private void Awake()
    {
        initHashmaps();
        document = GetComponent<UIDocument>();
        
        buttons = document.rootVisualElement.Query<Button>().ToList();
        sideMenu = document.rootVisualElement.Q("SideMenu");

        sideMenuCard = document.rootVisualElement.Q("ActivityName") as Label;
        sideMenuDescription = document.rootVisualElement.Q("DescriptionText") as Label;
        sideMenuImage = document.rootVisualElement.Q("ImagePlaceholder");
        sideMenuStatChanges = document.rootVisualElement.Q("StatChangePlaceholder");

        sideMenu.style.display = DisplayStyle.None;

        foreach(Button button in buttons)
        {
            button.RegisterCallback<ClickEvent>(onAllButtonClick);
            button.RegisterCallback<PointerEnterEvent>(onHoverButton);
            button.RegisterCallback<PointerLeaveEvent>(onStopHovering);
        }
    }

     private void statUI()
    {
        float normalizedMood = (StatTracker.instance.mood+50)/100.0f;
        float normalizedCharisma = (StatTracker.instance.charisma+50)/100.0f;
        float normalizedDiligence = (StatTracker.instance.dilligence+50)/100.0f;
        float normalizedHygeine = (StatTracker.instance.hygeine+50)/100.0f;

        Label diligenceField = document.rootVisualElement.Q("Diligence") as Label;
        Label charismaField = document.rootVisualElement.Q("Charisma") as Label;
        Label hygeineField = document.rootVisualElement.Q("Hygeine") as Label;
        Label moodField = document.rootVisualElement.Q("Mood") as Label;
        diligenceField.style.color = new Color(2.0f * (1.0f-normalizedDiligence), 2.0f * normalizedDiligence, 0);
        Debug.Log(normalizedDiligence + " " +2.0f*normalizedDiligence);
        charismaField.style.color = new Color(2.0f * (1.0f-normalizedCharisma), 2.0f * normalizedCharisma, 0);
        hygeineField.style.color = new Color(2.0f * (1.0f-normalizedHygeine), 2.0f * normalizedHygeine, 0);
        Debug.Log("h" + normalizedHygeine + " " +2.0f*normalizedHygeine);
        moodField.style.color = new Color(2.0f * (1.0f-normalizedMood), 2.0f * normalizedMood, 0);

        document.rootVisualElement.Q("Diligence").dataSource = StatTracker.instance;
        document.rootVisualElement.Q("Money").dataSource = StatTracker.instance;
        document.rootVisualElement.Q("Charisma").dataSource = StatTracker.instance;
        document.rootVisualElement.Q("Hygeine").dataSource = StatTracker.instance;
        document.rootVisualElement.Q("Day").dataSource = StatTracker.instance;
        document.rootVisualElement.Q("Mood").dataSource=StatTracker.instance;

       if(StatTracker.instance.timeElapsed==0)
        {
            document.rootVisualElement.Q("SunMoon").style.backgroundColor = new Color (0,0,0,0);
            document.rootVisualElement.Q("SunMoon").style.unityBackgroundImageTintColor = new Color (231.0f/255, 216.0f/255, 73.0f/255);
        }
        else if(StatTracker.instance.timeElapsed==1)
        {
            document.rootVisualElement.Q("SunMoon").style.backgroundColor = new Color (0,0,0,0);
            document.rootVisualElement.Q("SunMoon").style.unityBackgroundImageTintColor = new Color (179.0f/255, 165.0f/255, 43.0f/255);
        }
        else
        {
            document.rootVisualElement.Q("SunMoon").style.backgroundColor = new Color (0, 0 , 0, 255);
            document.rootVisualElement.Q("SunMoon").style.unityBackgroundImageTintColor = new Color (255/255, 255/255, 255/255);
        }
    }


    private void onAllButtonClick(ClickEvent evt)
    {
        Button button = evt.target as Button;
        Debug.Log(evt.target);
        
        if(!button.ClassListContains("notScene"))
        {
            SceneManager.LoadSceneAsync(button.name);
        }
    }
    private void onHoverButton(PointerEnterEvent evt)
    {
        Button button = evt.target as Button;
        Debug.Log(evt.target);

        sideMenuCard.text = buttonHoverMap[button.name].label;
            //sideMenuImage.style.backgroundImage = button.style.backgroundImage;
        sideMenuDescription.text = buttonHoverMap[button.name].description;

        sideMenu.style.display = DisplayStyle.Flex;
        
    }
    private void onStopHovering(PointerLeaveEvent evt)
    {
        sideMenu.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        foreach(Button button in buttons)
        {
            button.UnregisterCallback<ClickEvent>(onAllButtonClick);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatTracker.instance.eventChecker();
        statUI();
    }

    // Update is called once per frame
    void Update()
    {
        //StatTracker.instance.eventChecker();
        statUI();
    }
}
