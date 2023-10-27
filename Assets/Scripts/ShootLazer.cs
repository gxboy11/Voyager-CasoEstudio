using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLazer : MonoBehaviour
{
    [SerializeField]
    GameObject lazerPrefab;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    float force = 100.0F;

    [SerializeField]
    float fireTime;

    float _currentTime;

    Rigidbody rb;

    public AudioSource soundControl;
    public AudioClip ShootSound;
    public AudioSource soundControl2;
    public AudioClip destroySound;

    void Awake()
    {

        rb = lazerPrefab.GetComponent<Rigidbody>();
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= fireTime && Input.GetButton("Fire1"))
        {
            _currentTime = 0.0F;
            shoot();

            soundControl.PlayOneShot(ShootSound);

        }
    }

    void FixedUpdate()
    {
        rb.velocity = firePoint.forward * force;
    }

    void shoot()
    {
        Instantiate(lazerPrefab, firePoint.position, firePoint.rotation);
        soundControl.PlayOneShot(ShootSound);

    }
}
