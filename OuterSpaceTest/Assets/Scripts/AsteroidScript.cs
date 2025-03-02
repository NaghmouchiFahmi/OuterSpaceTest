using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public enum AsteroidType { Blue, Red, DP, yellow } 

    public AsteroidType type;

    [Header("Shockwave Settings")]
    public float shockwaveRadius = 50f; 
    public float shockwaveForce = 500f; 
    public float shockwaveUpwardModifier = 0.5f;
    public AudioClip shockwave;
    public AudioSource shockwaveSource;


    private void Start()
    {
        shockwaveSource.clip = shockwave;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandleBulletCollision();
            Destroy(gameObject);
        }
    }

    private void HandleBulletCollision()
    {
        switch (type)
        {
            case AsteroidType.Blue:
                GameManager.Instance.AddScore(5);
                break;

            case AsteroidType.Red:
                PlayerController player = FindObjectOfType<PlayerController>();
                if (player != null)
                {
                    player.ActivateInvincibility();
                }
                break;

            case AsteroidType.DP:
                shockwaveSource.Play();
                CreateShockwave();
                
                break;

            case AsteroidType.yellow: 
                ActivateSpeedBoost();
                break;
        }
    }


    private void CreateShockwave()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, shockwaveRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Asteroid"))
            {
                Destroy(nearbyObject.gameObject);
            }
            else
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    
                    rb.AddExplosionForce(shockwaveForce, transform.position, shockwaveRadius, shockwaveUpwardModifier, ForceMode.Impulse);
                }
            }
        }

        StartCoroutine(Shake(0.5f, 3f));
    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        Transform camTransform = Camera.main.transform; 
        Vector3 originalPosition = camTransform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 2f) * magnitude;
            float y = Random.Range(-1f, 2f) * magnitude;
            float z = Random.Range(-1f, 2f) * magnitude * 0.5f;

            camTransform.position = originalPosition + new Vector3(x, y, z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        camTransform.position = originalPosition;
    }


    private void ActivateSpeedBoost()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ActivateSpeedBoost(3f); 
        }
    }
}
