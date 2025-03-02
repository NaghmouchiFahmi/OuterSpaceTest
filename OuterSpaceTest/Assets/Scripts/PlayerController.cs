using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public float normalSpeed = 15f;
    public float CurrentSpeed = 0;
    public float InvisibilityTime = 10f;
    public float boostedSpeed = 10f;

    private Rigidbody rb;
    private Collider playerCollider;
    private Vector3 MoveDirections;
    public Transform SafeLocation;

    private bool isSpeedBoosted = false;
    private bool isInvincible = false;

    public GameObject InvisUi;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvisUi.SetActive(false);
        playerCollider= GetComponent<Collider>();
        CurrentSpeed = normalSpeed;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovements();
    }

    public void HandleMovements()
    {
        float XMove = Input.GetAxis("Horizontal");
        float ZMove = Input.GetAxis("Vertical");

        MoveDirections = new Vector3(XMove, 0, ZMove).normalized;


        if (MoveDirections != Vector3.zero)
        {
            
            Vector3 newPosition = rb.position + MoveDirections * CurrentSpeed * Time.deltaTime;
            rb.MovePosition(newPosition);

            Quaternion targetRotation = Quaternion.LookRotation(-MoveDirections);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 1f * Time.deltaTime));
        }

    }


    private void UpdateCameraPosition()
    {
        Vector3 cameraOffset = new Vector3(0, 10, -10); 
        Vector3 targetPosition = rb.position + cameraOffset;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, 0.1f);

    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject.CompareTag("Asteroid"))
        {
           
            
                GameManager.Instance.JumpCount--;
            Destroy(collision.gameObject);
            gameObject.transform.position = SafeLocation.position;
                
            
        }


    }


    public void ActivateInvincibility()
    {
        if (!isInvincible)
        {
            
            isInvincible = true;
            InvisUi.SetActive(true);

            playerCollider.isTrigger = true;
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        yield return new WaitForSeconds(InvisibilityTime);

        playerCollider.isTrigger = false;
        isInvincible = false;
        InvisUi.SetActive(false);



    }

    public void ActivateSpeedBoost(float duration)
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            CurrentSpeed = boostedSpeed;
            StartCoroutine(SpeedBoostCoroutine(duration));
        }
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        

        yield return new WaitForSeconds(duration);

        CurrentSpeed = normalSpeed;
        isSpeedBoosted = false;
        
    }



}
