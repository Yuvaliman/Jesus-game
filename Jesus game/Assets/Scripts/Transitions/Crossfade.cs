using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    public GameObject Loader;
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject image;

    public void CrossfadeAnimation(int scene)
    {
        Loader.SetActive(true);
        StartCoroutine(LoadScene(scene));
    }

    IEnumerator LoadScene(int scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }
}
