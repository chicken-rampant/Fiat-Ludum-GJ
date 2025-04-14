using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

public class MonitorMenuManager : MonoBehaviour
{
    private UIDocument document;
    private List<Button> buttons = new List<Button>();
    private VisualElement sideMenu, gachaMenu, sideMenuImage, sideMenuStatChanges;
    private Label sideMenuCard, sideMenuDescription;
    Dictionary<string, HoverBehaviorContainer> buttonHoverMap = new Dictionary<string, HoverBehaviorContainer>();
    
    int menuState;

    private void initHashmaps()
    {
        buttonHoverMap.Add("Friends", new HoverBehaviorContainer("Calling Friends", "A way to communicate with fellow human beings, increasing charisma."));
        buttonHoverMap.Add("Art", new HoverBehaviorContainer("Art Commissions", "A way to make some pocket change outside of your allowance. Scales with diligence. Also increases diligence"));
        buttonHoverMap.Add("SocialMedia", new HoverBehaviorContainer("Social Media", "Surf the interwebs. Almost anything can happen..."));
        buttonHoverMap.Add("LeetCode", new HoverBehaviorContainer("Leet Code", "A return to your CS major roots. Increases diligence greatly at the cost of mental stability."));
        buttonHoverMap.Add("Gacha", new HoverBehaviorContainer("Gacha", "Your addiction."));
        buttonHoverMap.Add("AzurLane", new HoverBehaviorContainer("Blue Avenue", "A game where ships are turned into waifus. The safest gacha, the one where the least can go wrong..."));
        buttonHoverMap.Add("Prosekai", new HoverBehaviorContainer("Noobsekai", "A rhythm game built off the backs of vocaldois. A nice in-between in terms of volatility. Higher highs but not tremendous lows."));
        buttonHoverMap.Add("Genshin", new HoverBehaviorContainer("G Game", "Created by M Company. A very popular open world game, and a step up from noobsekai in volatility. What you may get is anyone's guess."));
        buttonHoverMap.Add("FGO", new HoverBehaviorContainer("DM Order", "Destiny Monumentous Order. A grindy turn-player-based game, and the most volatile gacha. It is said that something special also lurks here..."));
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
        gachaMenu = document.rootVisualElement.Q("GachaPanel");

        sideMenuCard = document.rootVisualElement.Q("ActivityName") as Label;
        sideMenuDescription = document.rootVisualElement.Q("DescriptionText") as Label;
        sideMenuImage = document.rootVisualElement.Q("ImagePlaceholder");
        sideMenuStatChanges = document.rootVisualElement.Q("StatChangePlaceholder");

        if(!gachaMenu.ClassListContains("GachaPanelDefault"))
        {
            gachaMenu.AddToClassList("GachaPanelDefault");
        }
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
        float normalizedMood = (StatTracker.instance.mood+50)/100;
        float normalizedCharisma = (StatTracker.instance.charisma+50)/100;
        float normalizedDiligence = (StatTracker.instance.dilligence+50)/100;
        float normalizedHygeine = (StatTracker.instance.hygeine+50)/100;

        Label diligenceField = document.rootVisualElement.Q("Diligence") as Label;
        Label charismaField = document.rootVisualElement.Q("Charisma") as Label;
        Label hygeineField = document.rootVisualElement.Q("Hygeine") as Label;
        Label moodField = document.rootVisualElement.Q("Mood") as Label;
        diligenceField.style.color = new Color(2.0f * (1-normalizedDiligence), 2.0f * normalizedDiligence, 0);
        charismaField.style.color = new Color(2.0f * (1-normalizedCharisma), 2.0f * normalizedCharisma, 0);
        hygeineField.style.color = new Color(2.0f * (1-normalizedHygeine), 2.0f * normalizedHygeine, 0);
        moodField.style.color = new Color(2.0f * (1-normalizedMood), 2.0f * normalizedMood, 0);

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
        
        if(button.name.Equals("Gacha"))
        {
            gachaMenu.RemoveFromClassList("GachaPanelDefault");
            menuState=1;
        }
        else if (button.name.Equals("Back"))
        {
            gachaMenu.AddToClassList("GachaPanelDefault");
            menuState=0;
        }
        else if(!button.ClassListContains("notScene"))
        {
            SceneManager.LoadSceneAsync(button.name);
        }
    }
    private void onHoverButton(PointerEnterEvent evt)
    {
        Button button = evt.target as Button;
        Debug.Log(evt.target);

        if(!button.name.Equals("Back")&&!button.name.Equals("GameMenu"))
        {
            sideMenuCard.text = buttonHoverMap[button.name].label;
            //sideMenuImage.style.backgroundImage = button.style.backgroundImage;
            sideMenuDescription.text = buttonHoverMap[button.name].description;

            sideMenu.style.display = DisplayStyle.Flex;
        }
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
        menuState = 0;
        statUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuState==1)
            {
                gachaMenu.AddToClassList("GachaPanelDefault");
                menuState=0;
            }
            else if (menuState==0)
            {
                SceneManager.LoadSceneAsync("GameMenu");
            }
        }
        statUI();
    }
}

public class HoverBehaviorContainer
{
    public string description;
    public string label;
    public HoverBehaviorContainer(string label, string description)
    {
        this.description = description;
        this.label = label;
    }
}