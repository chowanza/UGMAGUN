using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

// Clase para controlar el movimiento del jugador
public class PlayerMovement : MonoBehaviour
{
    // Velocidad del jugador
    [SerializeField]
    private float _speed;

    // Borde de la pantalla
    [SerializeField]
    private float _screenBorder;

    // Componente Rigidbody2D del jugador
    private Rigidbody2D _rigidbody;
    // Entrada de movimiento del jugador
    private Vector2 _movementInput;
    // Última entrada de movimiento no nula del jugador
    public Vector2 _lastNonZeroMovementInput;
    // Cámara principal
    private Camera _camera;
    // Componente Animator del jugador
    private Animator _animator;

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 5f;
    private float lastDash = -5f;

    public int shotgunShotsRemaining;

    public TextMeshProUGUI cooldownText; // Referencia al texto del contador de enfriamiento en la UI
    public AudioSource dashSound; // Referencia al componente de sonido para el Dash

    // Método Awake se llama cuando se crea la instancia del script
    private void Awake() {
        Time.timeScale = 1;
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        _rigidbody.freezeRotation = true;
        // Establece la última dirección de movimiento no nula a la izquierda al principio del juego
        _lastNonZeroMovementInput = Vector2.left;
    }


    // FixedUpdate se llama una vez por frame, es útil para códigos de física
    private void Update() {
        SetPlayerVelocity();
        SetAnimation();
        if ((Keyboard.current.shiftKey.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame) && Time.time > lastDash + dashCooldown){
            StartCoroutine(Dash());
            dashSound.Play(); // Reproduce el sonido de Dash
            lastDash = Time.time;
        }

        // Actualiza el texto del contador de enfriamiento
        float cooldownRemaining = Mathf.Max(0, lastDash + dashCooldown - Time.time);
        cooldownText.text = "Cooldown: " + cooldownRemaining.ToString("0.0") + "s";
    }

    // Configura la animación del jugador
    private void SetAnimation(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f).normalized;
        _animator.SetFloat("Horizontal",movement.x);
        _animator.SetFloat("Vertical",movement.y);
        _animator.SetFloat("Magnitude",movement.magnitude);

        // Guarda la última dirección de movimiento si el jugador se está moviendo
        if (movement.x != 0 || movement.y != 0) {
            _animator.SetFloat("LastX",movement.x);
            _animator.SetFloat("LastY",movement.y);
        }
    }

    public void ActivateShotgunAbility(){
        shotgunShotsRemaining += 7;  // Inicializa el contador de disparos de escopeta
    }

    // Configura la velocidad del jugador
    private void SetPlayerVelocity() {
        _rigidbody.velocity = _movementInput * _speed;
        // Guarda la última entrada de movimiento si el jugador se está moviendo
        if (_movementInput != Vector2.zero) {
            _lastNonZeroMovementInput = _movementInput;
        }
        PreventPlayerGoingOffScreen();
    }

    // Previene que el jugador salga de la pantalla
    private void PreventPlayerGoingOffScreen(){
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        // Si el jugador está en el borde izquierdo o derecho de la pantalla, detiene el movimiento horizontal
        if ((screenPosition.x < _screenBorder && _rigidbody.velocity.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _screenBorder && _rigidbody.velocity.x > 0)){
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }

        // Si el jugador está en el borde superior o inferior de la pantalla, detiene el movimiento vertical
        if ((screenPosition.y < _screenBorder && _rigidbody.velocity.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _screenBorder && _rigidbody.velocity.y > 0)){
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }

    // Devuelve la última entrada de movimiento no nula
    public Vector2 GetLastNonZeroMovementInput()
    {
        return _lastNonZeroMovementInput;
    }

    
    // Se llama cuando se mueve el jugador
    private void OnMove(InputValue inputValue){
        _movementInput = inputValue.Get<Vector2>();
    }
    
    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            _rigidbody.velocity = _lastNonZeroMovementInput * dashSpeed;
            yield return null; // Espera hasta el próximo frame
        }
    }


}