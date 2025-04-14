using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

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
        else
        {
            SceneManager.LoadSceneAsync(button.name);
        }
    }
    private void onHoverButton(PointerEnterEvent evt)
    {
        Button button = evt.target as Button;
        Debug.Log(evt.target);

        if(!button.name.Equals("Gacha")&&!button.name.Equals("Back"))
        {
            sideMenuCard.text = button.name;
            sideMenuImage.style.backgroundImage = button.style.backgroundImage;

            //sideMenuDescription.text = buttonHoverMap[button.name].description;
            //sideMenuStatChanges.style.backgroundImage = Background.FromSprite(buttonHoverMap[button.name].statChanges);
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
    public Sprite statChanges;
    public string description;
    public string label;
    public HoverBehaviorContainer(Sprite statChanges, string description, string label)
    {
        this.statChanges = statChanges;
        this.description = description;
        this.label = label;
    }
}