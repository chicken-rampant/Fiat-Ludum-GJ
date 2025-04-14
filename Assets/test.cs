using UnityEngine;
using UnityEngine.SceneManagement;


public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadSceneAsync("GameMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
