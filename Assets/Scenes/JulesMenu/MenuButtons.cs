using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class MenuButtons : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject HomeScreen;
    public Slider slider;
    public Text progressText;
    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void WebSite()
    {
        Application.OpenURL("http://unity3d.com/");
    }

    public void LoadGame(string nameScene)
    {
        StartCoroutine(Load(nameScene));
    }

    IEnumerator Load(string nameScene)
    {

        loadingScreen.SetActive(true);
        HomeScreen.SetActive(false);
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
