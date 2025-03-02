using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningMechanic : MonoBehaviour
{
    public static SpawningMechanic Instance;
    public GameObject[] Asteroids;
    

    public float spawnInterval = 1f;
    public float minVelocity = 5f;
    public float maxVelocity = 30f;
    public float spawnRange = 30.0f;
    public float spawnStop = 30f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
        StartCoroutine(SpawningStopCoroutine());
    }


   
    private void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange), 
        transform.position.y,  
        transform.position.z  
        );

        Quaternion spawnRotation = Random.rotation; 

        GameObject objectPrefab = Asteroids[Random.Range(0, Asteroids.Length)];
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);

        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomSpeed = Random.Range(minVelocity, maxVelocity);
            rb.velocity = Vector3.back * randomSpeed; 
        }
    }



    private IEnumerator SpawningStopCoroutine()
    {
        yield return new WaitForSeconds(spawnStop);
        CancelInvoke(nameof(SpawnObject));


    }
    }
