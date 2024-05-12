using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TankController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _backwardsSpeedMultiplier;
    [SerializeField] private float _turnSpeed;
    private float _currentSpeed;

    private CharacterController _characterController;
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _currentSpeed = _walkSpeed;
    }

    private void Update()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Run") && verticalInput > 0)
        {
            _currentSpeed = _walkSpeed * _runSpeedMultiplier;
        }
        else
        {
            if (verticalInput < 0)
            {
                _currentSpeed = _walkSpeed * _backwardsSpeedMultiplier;
            }
            else
            {
                _currentSpeed = _walkSpeed;
            }            
        }

        var forwardVelocity = transform.forward * verticalInput * _currentSpeed * Time.deltaTime;

        _characterController.Move(forwardVelocity);

        transform.Rotate(0, horizontalInput * _turnSpeed * Time.deltaTime, 0);
    }
}
