using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public string scene;
    public void advanceScene()
    {
        SceneManager.LoadScene(scene);
    }

}
