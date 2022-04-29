using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public  event Action OnLoad;
    [SerializeField]int sceneIndex;
    Coroutine routine;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(routine==null)
            routine=StartCoroutine(loadNextScene());
    }
    IEnumerator loadNextScene()
    {
        OnLoad();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(sceneIndex);

    }
}
