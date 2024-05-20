using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Animator animator;
    public bool isAiming;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public Transform firePoint;
    private float nextFireTime = 0f;
    public float fireRate = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            animator.SetBool("isAiming", isAiming);
        }
        else if (Input.GetMouseButtonUp(1)) 
        {
            isAiming = false;
            animator.SetBool("isAiming", isAiming);
        }

        
        if (isAiming && Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        RaycastHit hit;

       
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            
            Vector3 targetPoint = hit.point;
            Vector3 direction = (targetPoint - firePoint.position).normalized;

            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(direction * bulletForce, ForceMode.Impulse);

            
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red, 2.0f);
        }
        else
        {
            
            Vector3 direction = Camera.main.transform.forward;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(direction * bulletForce, ForceMode.Impulse);

           
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100f, Color.red, 2.0f);
        }
    }
}
