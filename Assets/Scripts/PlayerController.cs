using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
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
    private bool _isShootable;

    [Header("Examine Objects")]
    [SerializeField] private TMP_Text _dialogueTextMesh;
    [SerializeField] private Canvas examineCanvas;
    private string _dialogueText;
    private Collider _currentCollider;
    private bool _isInteractable = false;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        // El jugador empieza caminando
        _currentSpeed = _walkSpeed;
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

    public void AimingAnim()
    {
        if (Input.GetButton("Aim"))
        {
            _isShootable = true;
            animator.SetBool("isAiming", _isShootable);
        }
        else
        {
            _isShootable = false;
            animator.SetBool("isAiming", _isShootable);
            Movement();
            if (_isInteractable && Input.GetButtonDown("Interact"))
            {
                UnityEngine.Debug.Log("Botón Pulsado");
                PrintDialogueLine(_currentCollider);
            }
        }
    }

    private void Update()
    {
        Rotation();
        AimingAnim();
    }

    // FUNCIÓN PARA PODER EXAMINAR OBJETOS


    public void PrintDialogueLine(Collider other)
    {
        if (checkPrintLine(other) != null)
        {
            StartCoroutine(CO_PrintDialogueLine(checkPrintLine(other), 0.06f));
            UnityEngine.Debug.Log("Examinar Ejecutado");
        }
        else
        {
            UnityEngine.Debug.Log("No hay objetos a examinar");
            return;
        }
    }

    private IEnumerator CO_PrintDialogueLine(string lineToPrint, float charSpeed)
    {
        _dialogueTextMesh.SetText(string.Empty);
        examineCanvas.gameObject.SetActive(true);

        for (int i = 0; i < lineToPrint.Length; i++)
        {
            var character = lineToPrint[i];
            _dialogueTextMesh.SetText(_dialogueTextMesh.text + character);
            yield return new WaitForSeconds(charSpeed);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        examineCanvas.gameObject.SetActive(false);
        _dialogueTextMesh.SetText(string.Empty);

        yield return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        _isInteractable = true;
        _currentCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        _isInteractable = false;
        _currentCollider = null;
    }

    private string checkPrintLine(Collider other)
    {            

        switch (other.gameObject.name)
        {
            case "Bed":
                return "It seems like this bed hasn't been used in days.";
            case "Mirror":
                return "I can't see my reflection because of the dust it's been collecting...";
            case "Closet":
                return "It's locked, I wonder what's in there...";
            case "DeskLamp":
                return "There's nothing interesting here.";
            case "StudyDesk":
                return "I couldn't study in a place like this. The nature sounds would interrupt me.";
            default:
                return null;
        }
    }
}
