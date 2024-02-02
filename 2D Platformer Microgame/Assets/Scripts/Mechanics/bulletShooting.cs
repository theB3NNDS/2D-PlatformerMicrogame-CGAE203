using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile1, projectile2, projectile3;
    public int currentProjectile = 0;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot(currentProjectile);
        }

        if (Input.GetButtonDown("Fire2")){
            currentProjectile = WeaponSwitch(currentProjectile);
        }

        void Shoot(int currentProjectile){
        //shooting logic
        if (currentProjectile == 0){
            Instantiate(projectile1, firePoint.position, firePoint.rotation);
        }
        else if (currentProjectile == 1){
            Instantiate(projectile2, firePoint.position, firePoint.rotation);
        }
        else if (currentProjectile == 2){
            Instantiate(projectile3, firePoint.position, firePoint.rotation);
        }
        }

        int WeaponSwitch(int currentProjectile){
        //whenever fire2 is pressed, increment and return currentProjectile which corresponds to a new bullet type
            currentProjectile++;
            if (currentProjectile == 3){
                currentProjectile = 0;
            }
            return currentProjectile;
        }

    }
}
