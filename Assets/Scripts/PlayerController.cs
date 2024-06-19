using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Tank Controller")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _backwardsSpeedMultiplier;
    [SerializeField] private float _turnSpeed;
    [SerializeField] public Animator animator;
    private float _currentSpeed; // Alternar entre caminar y correr

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        // El jugador empieza caminando
        _currentSpeed = _walkSpeed;
    }

    private void Start()
    {

    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    // Función para mover al personaje con controles tipo tanque
    public void Movement()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Run") && verticalInput > 0)
        {
            // Correr hacia adelante
            animator.SetBool("isRunning", true);
            _currentSpeed = _walkSpeed * _runSpeedMultiplier;
        }
        else
        {
            animator.SetBool("isRunning", false);
            // Validación para verificar si está caminando hacia atrás o hacia adelante
            if (verticalInput < 0)
            {
                _currentSpeed = _walkSpeed * _backwardsSpeedMultiplier;
            }
            else
            {
                animator.SetFloat("movement", verticalInput * _currentSpeed);
                _currentSpeed = _walkSpeed;
                
            }
        }

        var forwardVelocity = transform.forward * verticalInput * _currentSpeed * Time.deltaTime;

        //Mover al personaje
        _characterController.Move(forwardVelocity);

    }

    // Función para rotar el personaje
    public void Rotation()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        // Rotar al personaje
        animator.SetFloat("rotation", horizontalInput * _currentSpeed);
        transform.Rotate(0, horizontalInput * _turnSpeed * Time.deltaTime, 0);
    }
}
