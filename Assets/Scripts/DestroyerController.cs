using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}
