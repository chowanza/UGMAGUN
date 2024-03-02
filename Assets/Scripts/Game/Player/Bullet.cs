using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para controlar el comportamiento de una bala
public class Bullet : MonoBehaviour
{
    // Referencia a la cámara principal del juego
    private Camera _camera;

    // Método Awake se llama cuando se crea la instancia del script
    private void Awake(){
        // Obtiene la cámara principal del juego
        _camera = Camera.main;
    }

    // Update se llama una vez por frame
    private void Update(){
        // Destruye la bala cuando sale de la pantalla
        DestroyWhenOffScreen();
    }

    // OnTriggerEnter2D se llama cuando la bala entra en un trigger
    private void OnTriggerEnter2D(Collider2D collision){
        // Si el trigger pertenece a un enemigo
        if (collision.GetComponent<EnemyMovement>()){
            // Obtiene el controlador de salud del enemigo
            HealthController HealthController = collision.GetComponent<HealthController>();
            // Hace daño al enemigo
            HealthController.TakeDamage(10);
            // Destruye la bala
            Destroy(gameObject);
        }
    }

    // Destruye la bala cuando sale de la pantalla
    private void DestroyWhenOffScreen(){
        // Convierte la posición de la bala a coordenadas de pantalla
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        // Si la bala está fuera de la pantalla
        if((screenPosition.x < 0 || screenPosition.x > _camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > _camera.pixelHeight)){
            // Destruye la bala
            Destroy(gameObject);
        }
    }
}