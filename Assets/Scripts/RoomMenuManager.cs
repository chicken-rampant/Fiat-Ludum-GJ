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

    private void onAllButtonClick(ClickEvent evt)
    {
        Button button = evt.target as Button;
        Debug.Log(evt.target);
        
        SceneManager.LoadSceneAsync(button.name);
    }
    private void onHoverButton(PointerEnterEvent evt)
    {
        Button button = evt.target as Button;
        Debug.Log(evt.target);

        if(!button.name.Equals("MonitorMenu"))
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
