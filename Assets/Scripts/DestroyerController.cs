using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerController : MonoBehaviour
{
    [SerializeField]
    float forwardSpeed = 2.0f;

    Transform target; //Player

    Rigidbody _rb;

    Vector3 _position;


    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player"); //Busca al Player por su Tag
        target = go.GetComponent<Transform>();
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _position = target.transform.position;
    }

    private void FixedUpdate()
    {
        transform.LookAt(_position);

        _rb.position += transform.forward * forwardSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Enemigo ha tocado: " + other.gameObject.tag);
            // Se reinicia el juego
            Destroy(gameObject);
        }


    }
}
