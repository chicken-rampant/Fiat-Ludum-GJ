using UnityEngine;
using UnityEngine.UIElements;

public class TypewriterStarter : MonoBehaviour
{
    private UIDocument document;
    TypewriterLabel typewriterLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        document = GetComponent<UIDocument>();
        typewriterLabel = document.rootVisualElement.Q("typewritertest") as TypewriterLabel;
        typewriterLabel.setNewText("hello can we work");
        typewriterLabel.hideText();
        //StartCoroutine(typewriterLabel.autoIncrement());
    }


    // Update is called once per frame
    void Update()
    {
        /*if(typewriterLabel.incrementing)
        {
            typewriterLabel.increment();
            Debug.Log(typewriterLabel.sourceText);
            Debug.Log(typewriterLabel.textBox.text);
        }*/
    }
}
