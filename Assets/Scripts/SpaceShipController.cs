using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField]
    [Tooltip("Z Axis")]
    float forwardSpeed = 30.0f;

    [SerializeField]
    [Tooltip("X Axis")]
    float strafeSpeed = 8.0f; // Cuando nos movemos a la derecha o la izquierda

    [SerializeField]
    [Tooltip("Y Axis")]
    float hoverSpeed = 3.5f; //Movimiento con la nariz de la nave (arriba o abajo)

    [Header("Acceleration")] //Cuanto duramos de 0-1
    [SerializeField]
    float forwardAcceleration = 2.5f;

    [SerializeField]
    float strafeAcceleration = 2.0f;

    [SerializeField]
    float hoverAcceleration = 2.0f;

    [Header("Roll")]
    [SerializeField]
    float rollSpeed = 85.0f;

    [SerializeField]
    float rollAcceleration = 3.5f;

    Rigidbody _rb;

    float _activeForwardSpeed;
    float _activeStrafeSpeed;
    float _activeHoverSpeed;

    float _lookRateSpeed = 75.0f;

    float _rollInput;


    Vector2 _lookInput;
    Vector2 _screenCenter;
    Vector2 _mouseDistance;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }


    void Update()
    {
        HandleInputs();
    }

    void FixedUpdate()
    {

        transform.Rotate(-_mouseDistance.y * _lookRateSpeed * Time.fixedDeltaTime,
            _mouseDistance.x * _lookRateSpeed * Time.fixedDeltaTime,
            _rollInput * rollSpeed * Time.fixedDeltaTime,
            Space.Self);

        _rb.position += transform.forward * _activeForwardSpeed * Time.fixedDeltaTime; //FixedDeltaTime porque estamos haciendolo en el FixedUpdate
        _rb.position += transform.right * _activeStrafeSpeed * Time.fixedDeltaTime;
        _rb.position += transform.up * _activeHoverSpeed * Time.fixedDeltaTime;
    }

    void HandleInputs()
    {
        _lookInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _mouseDistance = new Vector2((_lookInput.x - _screenCenter.x) / _screenCenter.x,
            (_lookInput.y - _screenCenter.y) / _screenCenter.y);

        _mouseDistance = Vector2.ClampMagnitude(_mouseDistance, 1.0f); //si la magnitud de este vector es mayor a 1, entonces devuelvame un vector cuya magnitud sea 1

        _rollInput = Mathf.Lerp(_rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        float currentForwardSpeed = Input.GetAxisRaw("Forward") * forwardSpeed;
        _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed, currentForwardSpeed, forwardAcceleration * Time.deltaTime);

        float currentStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        _activeStrafeSpeed = Mathf.Lerp(_activeStrafeSpeed, currentStrafeSpeed, strafeAcceleration * Time.deltaTime);

        float currentHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed; //Edit / Project Settings / Input
        _activeHoverSpeed = Mathf.Lerp(_activeHoverSpeed, currentHoverSpeed, hoverAcceleration * Time.deltaTime);
    }
}
