using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Z Axis")]
    [SerializeField] float forwardSpeed = 30.0F;
    [Tooltip("X Axis")]
    [SerializeField] float strafeSpeed = 8.0F;
    [Tooltip("Y Axis")]
    [SerializeField] float hoverSpeed = 5.0F;



    [Header("Acceleration")]
    [SerializeField] float forwardAcceleration = 2.5F;
    [SerializeField] float strafeAcceleration = 2.0F;
    [SerializeField] float hoverAcceleration = 2.0F;

    [SerializeField]
    float minAcceleration = 1.0f;

    [Header("Roll")]
    [SerializeField] float rollSpeed = 85.0F;
    [SerializeField] float rollAcceleration = 1.5F;

    Rigidbody _rb;

    float _activeForwardSpeed;
    float _activeStrafeSpeed;
    float _activeHoverSpeed;
    float _lookRateSpeed = 75.0F;
    float _rollInput;



    Vector2 _lookInput;
    Vector2 _screenCenter;
    Vector2 _mouseDistance;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _screenCenter = new Vector2(Screen.width * 0.5F, Screen.height * 0.5F);
    }


    void Update()
    {
        HandleInputs();

    }
    void FixedUpdate()
    {
    
            if (!isMoving())
        {
            _rb.position += transform.forward * minAcceleration * Time.fixedDeltaTime;
        }

        transform.Rotate(-_mouseDistance.y * Time.fixedDeltaTime * _lookRateSpeed,
                          _mouseDistance.x * Time.fixedDeltaTime * _lookRateSpeed,
                          _rollInput * Time.fixedDeltaTime * rollSpeed,
                          Space.Self);
                          
        _rb.position += transform.forward * _activeForwardSpeed * Time.fixedDeltaTime;
        _rb.position += transform.right * _activeStrafeSpeed * Time.fixedDeltaTime;
        _rb.position += transform.up * _activeHoverSpeed * Time.fixedDeltaTime;

    }
    void HandleInputs()
    {
        _lookInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _mouseDistance = new Vector2((_lookInput.x - _screenCenter.x) / _screenCenter.x,
                                     (_lookInput.y - _screenCenter.y) / _screenCenter.y);

        _mouseDistance = Vector2.ClampMagnitude(_mouseDistance, 1.0F);

        _rollInput = Mathf.Lerp(_rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);



        float currentForwardSpeed = Input.GetAxisRaw("Forward") * forwardSpeed;
        _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed, currentForwardSpeed, forwardAcceleration * Time.deltaTime);

        float currentStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        _activeStrafeSpeed = Mathf.Lerp(_activeStrafeSpeed, currentStrafeSpeed, strafeAcceleration * Time.deltaTime);

        float currentHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;
        _activeHoverSpeed = Mathf.Lerp(_activeHoverSpeed, currentHoverSpeed, hoverAcceleration * Time.deltaTime);






        //_activeForwardSpeed = Input.GetAxisRaw("Vertical") * forwardSpeed;
        //_activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        //_activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;



    }





    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisi�n detectada con: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Destructor"))
        {
            // Se reinicia el juego
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Gatillo ingresado con: " + other.gameObject.tag);

        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Destructor"))
        {
            Debug.Log("Tocado: " + other.gameObject.tag);
            // Se reinicia el juego
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }

    bool isMoving()
    {
        if (_rb.velocity.magnitude <= 0)
        {
            return false;
        }
        return true;
    }
}

