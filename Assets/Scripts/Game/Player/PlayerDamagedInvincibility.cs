using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para controlar la invulnerabilidad del jugador después de recibir daño
public class PlayerDamagedInvincibility : MonoBehaviour
{
    // Duración de la invulnerabilidad del jugador
    [SerializeField]
    private float _invincibilityDuration;

    // Controlador de invulnerabilidad del jugador
    private InvincibilityController _invincibilityController;

    // Método Awake se llama cuando se crea la instancia del script
    private void Awake(){
        // Obtiene el componente InvincibilityController del jugador
        _invincibilityController = GetComponent<InvincibilityController>();
    }

    // Inicia la invulnerabilidad del jugador
    public void StartInvincibility(){
        // Inicia la invulnerabilidad con la duración especificada
        _invincibilityController.StartInvincibility(_invincibilityDuration);
    }
}