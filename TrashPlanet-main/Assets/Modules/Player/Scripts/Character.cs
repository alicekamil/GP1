using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Buffs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GP1.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        public void SetInput(CharacterInput input) => _input = input;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            UpdateInput();
            UpdateRotation();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
            UpdateHover();
            UpdateGravity();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<BasePickup>(out var pickup))
            {
                if (pickup is ObstaclePickup)
                {
                    PlayHitEffects();
                }
                pickup.Pickup();
            }
        }

        private void UpdateVelocity()
        {
            float speed = _speed + MovementManager.Instance.Multiplier * _extraSpeed;
            _rigidbody.velocity = speed * _currentMove * Vector3.right;
        }

        private void UpdateRotation()
        {
            var angles = _visuals.localEulerAngles;
            angles.y = _currentMove * _maxYRotation;
            angles.z = -_currentMove * _maxZRotation;
            _visuals.localEulerAngles = angles;
        }

        private void UpdateHover()
        {
            if (Physics.Raycast(_hoverPoint.position, Vector3.down, out var hitInfo, _hoverHeight + 0.1f, _groundMask))
            {
                float heightAboveGround = hitInfo.point.y + _hoverHeight;
                SetY(heightAboveGround);
                _isGrounded = true;
                _currentYVelocity = 0;
            }
            else
            {
                if (_wasGrounded)
                {
                    PlayJumpEffects();
                    _currentYVelocity = -_jumpVelocity;
                    _wasGrounded = false;
                }
                _isGrounded = false;
            }

            _wasGrounded = _isGrounded;
        }

        private void UpdateGravity()
        {
            if (!_isGrounded)
            {
                _currentYVelocity += _gravity * Time.deltaTime;
                SetY(transform.position.y - _currentYVelocity);
            }
        }
        
        private void UpdateInput()
        {
            float smooth = _moveSmooth + MovementManager.Instance.Multiplier * _extraSmooth;
            if (!_isGrounded)
                smooth *= _airMultiplier;
            _currentMove = Mathf.Lerp(_currentMove, _input.Move, Time.deltaTime * smooth);
        }

        private void SetY(float value)
        {
            var pos = transform.position;
            pos.y = value;
            _rigidbody.MovePosition(pos);
        }

        private void PlayHitEffects()
        {
            _hitClip.Play();
            _animator.SetTrigger("Hit");
        }

        private void PlayJumpEffects()
        {
            _cheerClip.Play();
            _animator.SetTrigger("Jump");
        }

        [SerializeField]
        private Transform _visuals;

        [Header("Movemenent")]
        [SerializeField, Min(0)]
        private float _speed;
        [SerializeField, Min(0)]
        private float _moveSmooth;
        [SerializeField]
        private float _extraSpeed;
        [SerializeField]
        private float _extraSmooth;
        [SerializeField]
        private float _airMultiplier;
        
        [Header("Rotation")]
        [SerializeField, Min(0)]
        private float _maxYRotation;
        [SerializeField, Min(0)]
        private float _maxZRotation;

        [Header("Hover")]
        [SerializeField]
        private float _hoverHeight;
        [SerializeField]
        private float _jumpVelocity;
        [SerializeField]
        private float _gravity;
        [SerializeField]
        private LayerMask _groundMask;
        [SerializeField]
        private Transform _hoverPoint;

        [Header("Audio")]
        [SerializeField]
        private AudioClipSO _hitClip;
        [SerializeField]
        private AudioClipSO _cheerClip;

        [Header("Animation")]
        [SerializeField]
        private Animator _animator;

        // Input
        private CharacterInput _input;
        // State
        private bool _isGrounded;
        private bool _wasGrounded;
        private float _currentYVelocity;
        private float _currentMove;
        // Components
        private Rigidbody _rigidbody;
    }
}