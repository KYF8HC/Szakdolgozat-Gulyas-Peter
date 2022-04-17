using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponPickup : MonoBehaviour
{
    public Weapons weaponScript;
    private System.Random rnd = new System.Random();
    public UnityEngine.Object Supplie;

    private void Start()
    {
        Supplie = Resources.Load("SupplieDrop");
        
        Debug.Log(Supplie);
    }
    private void Respawn()
    {
        GameObject SupplieClone = (GameObject)Instantiate(Supplie);
        SupplieClone.GetComponent<WeaponPickup>().weaponScript = gameObject.GetComponent<WeaponPickup>().weaponScript;
        SupplieClone.GetComponent<WeaponPickup>().Supplie = gameObject.GetComponent<WeaponPickup>().Supplie;
        //SupplieClone.transform.position = new Vector3(UnityEngine.Random.Range(-16f, -10f), UnityEngine.Random.Range(-4.8f, -2.8f), 0f);
        SupplieClone.transform.position = new Vector3(UnityEngine.Random.Range(-16f, -10f), -4.8f, 0f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        int weapon = rnd.Next(0, 2);
        if (other.tag == "Player" || other.tag == "AI")
        {
            switch (weapon)
            {
                case 0:
                    if (!weaponScript.LaserGun && !weaponScript.Pistol)
                    {
                        weaponScript.Pistol = true;
                        gameObject.SetActive(false);
                        Invoke("Respawn", 5);
                    }
                    break;
                case 1:
                    if (!weaponScript.LaserGun && !weaponScript.Pistol)
                    {
                        weaponScript.LaserGun = true;
                        gameObject.SetActive(false);
                        Invoke("Respawn", 5);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
