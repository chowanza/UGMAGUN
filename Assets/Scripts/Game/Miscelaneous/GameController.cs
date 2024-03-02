using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Puntaje necesario para ganar el juego
    //[SerializeField]
    //private int _winningScore = 100;

    // Referencia al controlador de puntaje
    [SerializeField]
    private ScoreController _scoreController;

    // Referencia al controlador de salud
    [SerializeField]
    private HealthController _healthController;

    // Update se llama una vez por frame
    private void Update()
    {
        // Si la vida del jugador es 0
        if (_healthController._currentHealth <= 0)
        {
            // Pierde el juego
            LoseGame();
        }
    }

    // Pierde el juego
    private void LoseGame()
    {
        // Muestra un mensaje de derrota
        Debug.Log("Has perdido el juego.");

        // Carga la escena de derrota (en proceso)
        SceneManager.LoadScene("Lose");
    }
}