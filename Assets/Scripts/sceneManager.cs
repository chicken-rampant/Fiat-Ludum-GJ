using System.Collections;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public Animator transition;
    public string scene;
    public float transitionTime = 1f;

    public void advanceScene()
    {
        StartCoroutine(advance(scene));
    }

    IEnumerator advance(string scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(scene);
    }
}