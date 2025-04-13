using UnityEngine;
using Yarn.Unity;

public class continueListener : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log("continue clicked");
            LineView lineView = GameObject.FindAnyObjectByType<LineView>();
            lineView.OnContinueClicked();
        }
    }
}
