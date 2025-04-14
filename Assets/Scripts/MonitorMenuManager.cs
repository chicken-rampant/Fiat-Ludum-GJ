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