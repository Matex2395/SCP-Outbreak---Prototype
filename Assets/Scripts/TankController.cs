using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requerir siempre un CharacterController, para que no se elimine y evitar errores
[RequireComponent(typeof(CharacterController))]
public class TankController : MonoBehaviour
{
    [Header("Tank Controller")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _backwardsSpeedMultiplier;
    [SerializeField] private float _turnSpeed;
    private float _currentSpeed; // Alternar entre caminar y correr

    private CharacterController _characterController;

    [Header("Aiming")]
    [SerializeField] public GameObject RightArm;

    [Header("Shooting")]
    public GameObject BulletOrigin; // Punto de inicio de la Instancia
    public GameObject BulletPrefab; // Prefab de la Bala
    public float BulletSpeed; // Velocidad de Bala
    private bool _isShootable;
    public float FireCooldown;
    private float _currentCooldown;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        // El jugador empieza caminando
        _currentSpeed = _walkSpeed;
    }

    private void Start()
    {
        _currentCooldown = FireCooldown;
    }

    private void Update()
    {
        AimingAnim();
        Shoot();
        Rotation();
    }

    // Función para mover al personaje con controles tipo tanque
    public void Movement()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");

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
        
    }

    // Función para rotar el personaje
    public void Rotation()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        // Rotar al personaje
        transform.Rotate(0, horizontalInput * _turnSpeed * Time.deltaTime, 0);
    }

    // Función para comenzar animaciones
    public void AimingAnim()
    {
        if (Input.GetButtonDown("Aim"))
        {
            RightArm.GetComponent<Animator>().Play("ArmAimingAnimation");
        }

        if (Input.GetButtonUp("Aim"))
        {
            RightArm.GetComponent<Animator>().Play("New State");
        }
    }

    // Función para disparar el arma
    public void Shoot()
    {
        if (Input.GetButton("Aim"))
        {
            _isShootable = true;
        }
        else
        {
            _isShootable = false;
        }

        if (_isShootable)
        {

            if (Input.GetButtonDown("Shoot"))
            {
                if (_currentCooldown <= 0f)
                {
                    //Instanciar el Prefab de la Bala en BulletOrigin
                    GameObject TempBullet = Instantiate(BulletPrefab, BulletOrigin.transform.position, BulletOrigin.transform.rotation) as GameObject;
                    //Obtener Rigidbody para agregar fuerza
                    Rigidbody rb = TempBullet.GetComponent<Rigidbody>();
                    //Agregar fuerza a la bala
                    rb.AddForce(transform.forward * BulletSpeed);
                    //Destruir bala después de un tiempo
                    Destroy(TempBullet, 5.0f);
                    _currentCooldown = FireCooldown;
                }
            }
            _currentCooldown -= Time.deltaTime;
        } else
        {
            Movement();
        }
    }
}
