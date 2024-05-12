using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requerir siempre un CharacterController, para que no se elimine y evitar errores
[RequireComponent(typeof(CharacterController))]
public class TankController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _backwardsSpeedMultiplier;
    [SerializeField] private float _turnSpeed;
    private float _currentSpeed; // Alternar entre caminar y correr

    private CharacterController _characterController;
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        // El jugador empieza caminando
        _currentSpeed = _walkSpeed;
    }

    private void Update()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Run") && verticalInput > 0)
        {
            // Correr hacia adelante
            _currentSpeed = _walkSpeed * _runSpeedMultiplier;
        }
        else
        {
            // Validación para verificar si está caminando hacia atrás o hacia adelante
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

        //Mover al personaje
        _characterController.Move(forwardVelocity);
        // Rotar al personaje
        transform.Rotate(0, horizontalInput * _turnSpeed * Time.deltaTime, 0);
    }
}
