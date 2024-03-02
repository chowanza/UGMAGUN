using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoopVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public string nextSceneName = "NextScene";

    void Start()
    {
        // Configura el video para que se repita
        videoPlayer.isLooping = true;

        // Comienza a reproducir el video
        videoPlayer.Play();

        // Comienza a reproducir el sonido
        audioSource.Play();

        // Si se presiona la barra espaciadora, carga la siguiente escena
        StartCoroutine(CheckForSkip());
    }

    IEnumerator CheckForSkip()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextScene();
                yield break;
            }

            yield return null;
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}