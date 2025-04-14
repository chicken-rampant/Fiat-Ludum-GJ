using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewMonoBehaviourScript : MonoBehaviour
{
    private UIDocument document;
    private UnityEngine.UIElements.Button button;
    GameObject music;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        music = GameObject.FindWithTag("Music");
        document = GetComponent<UIDocument>();
        button = document.rootVisualElement.Q("startbutton") as UnityEngine.UIElements.Button;  
        button.RegisterCallback<ClickEvent>(onPlayGameClicked);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(onPlayGameClicked);
    }

    private void onPlayGameClicked(ClickEvent evt)
    {
        DontDestroyOnLoad(music);
        SceneManager.LoadSceneAsync("IntroDialogue");
    }
}
