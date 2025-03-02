using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public float bulletSpeed = 20f;
    private int ShotFired = 0;
    public int maxShots = 0;
    public float cooldownTime = 5f;
    private bool isCooldown = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCooldown) 
        {
            Fire();
        }
       
    }



    void Fire()
    {
        if (ShotFired < maxShots)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            bullet.GetComponent<Rigidbody>().AddForce(-firePoint.forward * bulletSpeed, ForceMode.Impulse);
            Debug.DrawRay(firePoint.position, -firePoint.forward * 10, Color.red, 2f);
            Destroy(bullet, 2f);

            ShotFired++;

            if (ShotFired >= maxShots)
            {
                StartCoroutine(Cooldown());
            }
        }
    }


    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        ShotFired = 0;
        isCooldown = false;
    }


}
