using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string menuSceneName = "Menu";

    void Start()
    {
        // Comienza a reproducir el video
        videoPlayer.Play();

        // Cuando el video termine, carga la escena del menú
        videoPlayer.loopPointReached += LoadMenuScene;

        // Si se presiona cualquier tecla, carga la escena del menú
        StartCoroutine(CheckForSkip());
    }

    IEnumerator CheckForSkip()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(menuSceneName);
                yield break;
            }

            yield return null;
        }
    }

    void LoadMenuScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(menuSceneName);
    }
}