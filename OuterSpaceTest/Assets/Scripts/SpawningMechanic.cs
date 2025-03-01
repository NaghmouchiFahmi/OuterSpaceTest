using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningMechanic : MonoBehaviour
{
    public GameObject BlueAST;
    public GameObject RedAST;
    public float spawnInterval = 1f;
    private float minVelocity = 10f;
    private float maxVelocity = 50f;
    public float speed = 10f;
    public float spawnRange = 30.0f;
    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange), 
        transform.position.y,  
        transform.position.z  
        );

        Quaternion spawnRotation = Random.rotation; 

        GameObject objectPrefab = Random.Range(0, 2) == 0 ? BlueAST : RedAST;
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);

        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomSpeed = Random.Range(minVelocity, maxVelocity);
            rb.velocity = Vector3.back * randomSpeed; 
        }
    }
}
