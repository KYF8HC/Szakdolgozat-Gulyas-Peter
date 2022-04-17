using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Start()
    {
        InvokeRepeating("Shoot", 5.0f, 1.0f);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position,  firePoint.rotation);
    }
}
