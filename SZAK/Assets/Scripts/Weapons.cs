using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 40;
    public LineRenderer lineRenderer;
    public GameObject bulletPrefab;
    public bool LaserGun = false;
    public bool Pistol = false;
    public GameObject pistol;
    public GameObject laserGun;

    void Update()
    {
        if (LaserGun && !Pistol)
        {
            laserGun.SetActive(true);
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    StartCoroutine(Shoot());
            //    laserGun.SetActive(false);
            //    LaserGun = false;
            //}
        }
        if (Pistol && !LaserGun)
        {
            pistol.SetActive(true);
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    PistolShoot();
            //    pistol.SetActive(false);
            //    Pistol = false;
            //}
        }
    }

    public IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo)
        {
            Player enemy = hitInfo.transform.GetComponent<Player>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        yield return new WaitForSeconds(0.5f);

        lineRenderer.enabled = false;
    }

    public void PistolShoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
