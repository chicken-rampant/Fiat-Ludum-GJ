using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class NewMonoBehaviourScript : MonoBehaviour
{
    private UIDocument document;
    private Button button;
    public GameObject transition;
    GameObject music;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        music = GameObject.FindWithTag("Music");
        document = GetComponent<UIDocument>();
        button = document.rootVisualElement.Q("startbutton") as Button;  
        button.RegisterCallback<ClickEvent>(onPlayGameClicked);
        transition = GameObject.FindWithTag("Transition");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log("continue clicked");
            DontDestroyOnLoad(music);
            SceneManager.LoadSceneAsync("IntroDialogue");        
        }
    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(onPlayGameClicked);
    }

    private void onPlayGameClicked(ClickEvent evt)
    {
        DontDestroyOnLoad(music);
        transition.GetComponent<sceneManager>().advanceScene();
    }
}
