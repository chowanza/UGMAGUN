using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Clase para controlar el disparo del jugador
public class PlayerShoot : MonoBehaviour
{
    // Prefab de la bala
    [SerializeField]
    private GameObject _bulletPrefab;

    // Script de movimiento del jugador
    [SerializeField]
    private PlayerMovement _playerMovement;

    // Velocidad de la bala
    [SerializeField]
    private float _bulletSpeed;

    // Offset de la pistola cuando el jugador mira a la derecha
    [SerializeField]
    private Transform _gunOffsetRight;

    // Offset de la pistola cuando el jugador mira a la izquierda
    [SerializeField]
    private Transform _gunOffsetLeft;
    
    // Offset de la pistola cuando el jugador mira hacia arriba
    [SerializeField]
    private Transform _gunOffsetUp;

    // Offset de la pistola actual
    private Transform _currentGunOffset;

    // Tiempo entre disparos
    [SerializeField]
    private float _timeBetweenShots;

    // Si el jugador está disparando continuamente
    private bool _fireContinuously;
    // Si el jugador está disparando una sola vez
    private bool _fireSingle;
    // Última vez que el jugador disparó
    private float _lastFireTime;

    // Asume que tienes un AudioSource en tu personaje llamado audioSource
    public AudioSource audioSource;
    // Asume que tienes un AudioClip para el sonido del disparo llamado gunshotSound
    public AudioClip gunshotSound;

    // Update se llama una vez por frame
    void Update(){
        // Determina la dirección en la que el personaje está mirando
        if (_playerMovement.GetLastNonZeroMovementInput().y > 0) {
            _currentGunOffset = _gunOffsetUp;
        } else if (_playerMovement.GetLastNonZeroMovementInput().x > 0) {
            _currentGunOffset = _gunOffsetRight;
        } else {
            _currentGunOffset = _gunOffsetLeft;
        }
        // Si el jugador está disparando, verifica si ha pasado suficiente tiempo desde el último disparo
        if (_fireContinuously || _fireSingle){
            float timeSinceLastFire = Time.time - _lastFireTime;

            // Si ha pasado suficiente tiempo, dispara una bala
            if (timeSinceLastFire >= _timeBetweenShots){
                
                FireBullet();
                _lastFireTime = Time.time;
                _fireSingle = false;
            }
    
        }
    }

    // Dispara una bala
    private void FireBullet(){
        // Crea una bala en la posición del offset de la pistola actual
        GameObject bullet = Instantiate(_bulletPrefab, _currentGunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        // Establece la velocidad de la bala en la dirección del último movimiento no nulo del jugador
        Vector2 bulletDirection = _playerMovement.GetLastNonZeroMovementInput();
        rigidbody.velocity = _bulletSpeed * bulletDirection;

        // Si el jugador tiene la habilidad de escopeta, dispara dos balas adicionales
        if (_playerMovement.shotgunShotsRemaining > 0){
            // Crea las balas adicionales
            GameObject bullet1 = Instantiate(_bulletPrefab, _currentGunOffset.position, Quaternion.Euler(0, 0, -30) * transform.rotation);
            GameObject bullet2 = Instantiate(_bulletPrefab, _currentGunOffset.position, Quaternion.Euler(0, 0, 30) * transform.rotation);

            // Establece la velocidad de las balas adicionales
            Rigidbody2D rigidbody1 = bullet1.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidbody2 = bullet2.GetComponent<Rigidbody2D>();
            rigidbody1.velocity = _bulletSpeed * (Quaternion.Euler(0, 0, -30) * bulletDirection);
            rigidbody2.velocity = _bulletSpeed * (Quaternion.Euler(0, 0, 30) * bulletDirection);

            // Decrementa el contador de disparos de escopeta
            _playerMovement.shotgunShotsRemaining--;
        }

        // Reproduce el sonido del disparo
        audioSource.PlayOneShot(gunshotSound);
    }

    // Se llama cuando el jugador dispara
    private void OnFire(InputValue inputValue){
         _fireContinuously = inputValue.isPressed;
    }
}