using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public enum AsteroidType { Blue, Red }
    public AsteroidType type;
    public float speed = 5f;



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
                Debug.Log("Blue Asteroid Destroyed! Points: " + 5);
                break;

            case AsteroidType.Red:
                PlayerController player = FindObjectOfType<PlayerController>();
                if (player != null)
                {
                    player.ActivateInvincibility();
                }
                Debug.Log("Red Asteroid Destroyed! Invincibility Activated for "  + " seconds.");
                break;
        }
    }
}
