using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        // Asegúrate de que el botón esté configurado en el Inspector de Unity
        exitButton.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        // Si estás en el editor de Unity, esto cerrará el modo de reproducción
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Si estás en una compilación del juego, esto cerrará la aplicación
        Application.Quit();
        #endif
    }
}
