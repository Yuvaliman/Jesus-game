using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject image;

    public void CrossfadeAnimation(int scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    IEnumerator LoadScene(int scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }
}
