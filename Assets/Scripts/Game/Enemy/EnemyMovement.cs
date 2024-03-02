using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    
    [SerializeField]
    private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private Camera _camera;
    private Animator _animator;


    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
        _targetDirection = Vector2.left;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        MoveTowardsTarget();
        SetVelocity();
        SetAnimation();
    }

    private void UpdateTargetDirection(){
            HandleRandomDirectionChange();
            HandleEnemyOffScreen();
    }

    private void HandleRandomDirectionChange(){
        _changeDirectionCooldown -= Time.deltaTime;
        
        if (_changeDirectionCooldown <= 0){
            float angleChange = Random.Range(-90f, 90f);
            Vector2 directionChange = new Vector2(Mathf.Cos(angleChange), Mathf.Sin(angleChange));
            _targetDirection = directionChange.normalized;

            _changeDirectionCooldown = Random.Range(1f,5f);
        }
    }


    private void HandleEnemyOffScreen(){
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

          if ((screenPosition.x < _screenBorder && _targetDirection.x < 0) ||
             (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0)){
              _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
          }

          if ((screenPosition.y < _screenBorder && _targetDirection.y < 0) ||
             (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0)){
               _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
          }
    }

    private void MoveTowardsTarget() {
        Vector2 direction = _targetDirection - (Vector2)transform.position;
        direction.Normalize();
        _rigidbody.velocity = direction * _speed;
    }

    private void SetAnimation(){
        Vector3 movement = _targetDirection.normalized;
        _animator.SetFloat("Horizontal",movement.x);
        _animator.SetFloat("Vertical",movement.y);
    }


    private void SetVelocity(){

        if (_playerAwarenessController.AwareOfPlayer){
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        _rigidbody.velocity = _targetDirection * _speed;
        

    }
}
