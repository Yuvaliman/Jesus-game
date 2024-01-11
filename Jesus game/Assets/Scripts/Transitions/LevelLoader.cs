using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public Animator transition;
    public float transitionTime = 1f;

    public void Load(int sceneIndex)
    {
        transition.SetTrigger("Start");
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress) / .9f;

            slider.value = progress;
            progressText.text = progress * 100 + "%";

            yield return null;
        }
    }
}
